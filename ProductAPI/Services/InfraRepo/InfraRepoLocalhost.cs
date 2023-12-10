using System.Net;

namespace ProductAPI.Services;


public class InfraRepoLocalhost : IInfraRepo {

    private readonly HttpClient _httpClient;
    private readonly ILogger<InfraRepoLocalhost> _logger;
    public InfraRepoLocalhost(ILogger<InfraRepoLocalhost> logger){
        _logger = logger;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost/")
        };
    }
    public async Task<HttpStatusCode> authenticateUser(string token){
        // Sæt headeren
        _logger.LogInformation("authenticateUser | Token:" + token);
        _httpClient.DefaultRequestHeaders.Add("Authorization", token);
        var response = await _httpClient.GetAsync("auth/authorize/");
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> doesUserExist(string userId, string token){
        _logger.LogInformation("doesUserExist | userId:" + userId);
         try{
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var response = await _httpClient.GetAsync("users/" + userId);
            return response.StatusCode;
        }
        catch(Exception e){
            throw new Exception("Error in InfraRepoLocalhost.GetuserHash: " + e.Message);
        }
    }




}
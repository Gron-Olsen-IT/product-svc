using System.Net;

namespace ProductAPI.Services;


public class InfraRepoLocalhost : IInfraRepo {

    private readonly HttpClient httpClient;
    private readonly ILogger<InfraRepoLocalhost> _logger;
    public InfraRepoLocalhost(ILogger<InfraRepoLocalhost> logger){
        _logger = logger;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost/")
        };
    }
    public async Task<HttpStatusCode> authenticateUser(string token){
        // Sæt headeren
        _logger.LogInformation("authenticateUser | Token:" + token);
        httpClient.DefaultRequestHeaders.Add("Authorization", token);
        var response = await httpClient.GetAsync("auth/authorize/");
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> doesUserExist(string userId){
        _logger.LogInformation("doesUserExist | userId:" + userId);
         try{
            var response = await httpClient.GetAsync("users/" + userId);
            return response.StatusCode;
        }
        catch(Exception e){
            throw new Exception("Error in InfraRepoLocalhost.GetuserHash: " + e.Message);
        }
    }




}
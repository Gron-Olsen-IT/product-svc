using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Services;


public class InfraRepo : IInfraRepo {
    private readonly string INFRA_CONN;
    private readonly HttpClient _httpClient;
    private readonly ILogger<InfraRepo> _logger;

    public InfraRepo(ILogger<InfraRepo> logger, IConfiguration configuration){
        _logger = logger;
        try{
            INFRA_CONN = configuration["INFRA_CONN"]!;
        }catch(Exception e){
            throw new Exception("INFRA_CONN is not set : " + e.Message);
        }
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(INFRA_CONN)
        };
    }
    public async Task<HttpStatusCode> authenticateUser(string token){
        // Sæt headeren
        _logger.LogInformation("authenticateUser | Token:" + token);
        _httpClient.DefaultRequestHeaders.Add("Authorization", token);
        var response = await _httpClient.PostAsync("/auth/authorize/", null);
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> doesUserExist(string userId, string token){
        _logger.LogInformation("doesUserExist | userId: " + userId.Count());
         try{
            _logger.LogInformation("doesUserExist | token: " + token.Count());
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var response = await _httpClient.GetAsync("/users/" + userId);
            _logger.LogInformation("doesUserExist | response: " + response.ToString());
            return response.StatusCode;
        }
        catch(Exception e){
            throw new Exception("Error in InfraRepoDocker.GetuserHash: " + e.Message);
        }
    }

    





}
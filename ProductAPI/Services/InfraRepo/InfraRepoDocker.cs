using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Services;


public class InfraRepoDocker : IInfraRepo {
    private readonly HttpClient _httpClient;
    private readonly ILogger<InfraRepoDocker> _logger;

    public InfraRepoDocker(ILogger<InfraRepoDocker> logger){
        _logger = logger;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://nginx:4000/")
        };
    }
    public async Task<HttpStatusCode> authenticateUser(string token){
        // Sæt headeren
        _logger.LogInformation("authenticateUser | Token:" + token);
        _httpClient.DefaultRequestHeaders.Add("Authorization", token);
        var response = await _httpClient.PostAsync("auth/authorize/", null);
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> doesUserExist(string userId, string token){
        _logger.LogInformation("doesUserExist | userId: " + userId.Count());
         try{
            _logger.LogInformation("doesUserExist | token: " + token.Count());
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var response = await _httpClient.GetAsync("users/" + userId);
            _logger.LogInformation("doesUserExist | response: " + response.ToString());
            return response.StatusCode;
        }
        catch(Exception e){
            throw new Exception("Error in InfraRepoDocker.GetuserHash: " + e.Message);
        }
    }

    





}
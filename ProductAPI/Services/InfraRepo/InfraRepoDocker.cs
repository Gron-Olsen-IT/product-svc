using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Services;


public class InfraRepoDocker : IInfraRepo {
    private readonly HttpClient httpClient;
    private readonly ILogger<InfraRepoDocker> _logger;

    public InfraRepoDocker(ILogger<InfraRepoDocker> logger){
        _logger = logger;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://nginx:4000/")
        };
    }
    public async Task<HttpStatusCode> authenticateUser(string token){
        // Sæt headeren
        _logger.LogInformation("authenticateUser | Token:" + token);
        httpClient.DefaultRequestHeaders.Add("Authorization", token);
        var response = await httpClient.GetAsync("auth/verify/");
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> doesUserExist(string userId){
        _logger.LogInformation("doesUserExist | userId:" + userId);
         try{
            var response = await httpClient.GetAsync("users/" + userId);
            return response.StatusCode;
        }
        catch(Exception e){
            throw new Exception("Error in InfraRepoDocker.GetuserHash: " + e.Message);
        }
    }

    





}
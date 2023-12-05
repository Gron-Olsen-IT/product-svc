using System.Net;

namespace ProductAPI.Services;


public class InfraRepoDocker : IInfraRepo {

    public async Task<HttpStatusCode> verifyUser(string email, string token){
        HttpClient httpClient = new HttpClient();
        // Sæt headeren
        httpClient.DefaultRequestHeaders.Add("JWT_TOKEN", token);
        httpClient.DefaultRequestHeaders.Add("EMAIL", email);
        var response = await httpClient.GetAsync("http://nginx:4000/auth/verify/" + email);
        return response.StatusCode;
    }





}
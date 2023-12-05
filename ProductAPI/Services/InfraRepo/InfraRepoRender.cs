using System.Net;

namespace ProductAPI.Services;


public class InfraRepoRender : IInfraRepo {

    public async Task<HttpStatusCode> authenticateUser(string token){
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("JWT_TOKEN", token);
        var response = await httpClient.GetAsync("http://localhost:4000/user/");
        return response.StatusCode;
    }


    public async Task<HttpStatusCode> doesUserExist(string email){
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("EMAIL", email);
        var response = await httpClient.GetAsync("http://localhost:4000/user/");
        return response.StatusCode;
    }




}
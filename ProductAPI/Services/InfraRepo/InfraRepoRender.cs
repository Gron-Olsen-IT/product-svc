using System.Net;

namespace ProductAPI.Services;


public class InfraRepoRender : IInfraRepo {

    public async Task<HttpStatusCode> verifyUser(string userId, string token){
        HttpClient httpClient = new HttpClient();
        var response = await httpClient.GetAsync("http://localhost:4000/user/" + userId);
        return response.StatusCode;
    }





}
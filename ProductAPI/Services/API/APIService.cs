using System.Net;

namespace ProductAPI.Services;


public class APIService : IAPIService {

    public async Task<HttpStatusCode> verifyUser(string userId){
        HttpClient httpClient = new HttpClient();
        var response = await httpClient.GetAsync("http://localhost:4000/user/" + userId);
        return response.StatusCode;
    }





}
namespace ProductAPI.Services;

using System.Net;
public interface IAPIService {
    public Task<HttpStatusCode> verifyUser(string userId);
}
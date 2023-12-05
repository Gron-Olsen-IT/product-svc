namespace ProductAPI.Services;

using System.Net;
public interface IInfraRepo {
    public Task<HttpStatusCode> verifyUser(string userId, string token);
}
namespace ProductAPI.Services;

using System.Net;
public interface IInfraRepo {
    public Task<HttpStatusCode> authenticateUser(string token);

    public Task<HttpStatusCode> doesUserExist(string email);
}
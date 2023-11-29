using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Services;
public interface IProductService
{
    public Task<List<Product>> Get();

    public Task<Product> Get(string id);

    public Task<List<Product>> Get(List<string> ids);

    public Task<HttpStatusCode> Post([FromBody] Product product);

    public Task<HttpStatusCode> Put(string id, [FromBody] Product product);
}
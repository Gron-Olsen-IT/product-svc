using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Services;
public interface IProductRepository
{
    public Task<List<Product>> Get();

    public Task<Product> Get(string id);

    public Task<List<Product>> Get(List<string> ids);

    public Task<Product> Post([FromBody] Product product);

    public Task<HttpStatusCode> Put([FromBody] Product product);
}
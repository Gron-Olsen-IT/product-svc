using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Services;
public interface IProductService
{
    public Task<List<Product>> Get();

    public Task<Product> Get(string id);

    public Task<List<Product>> Get(List<string> ids);

    public Task<Product> Post([FromBody] ProductDTO product);

    public Task<Product> Put([FromBody] Product product);
}
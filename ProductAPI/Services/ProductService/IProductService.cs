using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Services;
public interface IProductService
{
    public Task<List<Product>> Get();

    public Task<Product> Get(string id);

    public Task<List<Product>> Get(List<string> ids);

    public Task<Product> Post(ProductDTO productDTO, string token);

    public Task<Product> Put(Product product, string token);

    public Task<HttpStatusCode> Delete(string id);
}
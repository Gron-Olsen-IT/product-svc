using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Services;
public class ProductService : IProductService
{
    public Task<List<Product>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<Product> Get(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> Post([FromBody] Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> Put(string id, [FromBody] Product product)
    {
        throw new NotImplementedException();
    }
}
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Services;
public interface IProductService
{
    public Task<List<Product>> Get();

    public Task<Product> Get(string id);

    public Task<IActionResult> Post([FromBody] Product product);

    public Task<IActionResult> Put(string id, [FromBody] Product product);
}
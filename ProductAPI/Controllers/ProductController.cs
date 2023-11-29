using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service = new ProductService();
    [HttpGet]
    public async Task<List<Product>> Get()
    {
        return await _service.Get();
    }

    [HttpGet("{id}")]
    public async Task<Product> Get(string id)
    {
        return await _service.Get(id);
    }

    [HttpPost]
    public void Post([FromBody] Product product)
    {
        _service.Post(product);
    }

    [HttpPut("{id}")]
    public void Put(Guid id, [FromBody] Product product)
    {
    }

}

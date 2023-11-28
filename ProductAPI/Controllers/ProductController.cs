using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return new List<Product>();
    }

    [HttpGet("{id}")]
    public Product Get(string id)
    {
        throw new NotImplementedException("Not implemented yet");
    }

    [HttpPost]
    public void Post([FromBody] Product product)
    {
        _service.Post(product);
    }

    [HttpPut("{id}")]
    public void Put(string id, [FromBody] Product product)
    {
    }

}

using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return new List<Product>();
    }

    [HttpGet("{id}")]
    public Product Get(string id)
    {
        return new Product();
    }

    [HttpPost]
    public void Post([FromBody] Product product)
    {
    }

    [HttpPut("{id}")]
    public void Put(string id, [FromBody] Product product)
    {
    }

}

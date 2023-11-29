using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service = new ProductService();
    private readonly ILogger<ProductController> _logger;


    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
        var hostName = System.Net.Dns.GetHostName();
        var ips = System.Net.Dns.GetHostAddresses(hostName);
        var _ipaddr = ips.First().MapToIPv4().ToString();
        _logger.LogInformation(1, $"ProductService responding from {_ipaddr}");
    }



    [HttpGet]
    public async Task<List<Product>> Get()
    {
        try
        {
            return await _service.Get();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Get Testing moree");
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<Product> Get(string id)
    {
        try
        {
            return await _service.Get(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Get");
            throw;
        }
    }

    [HttpPost]
    public void Post([FromBody] Product product)
    {
        try
        {
            _service.Post(product);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Post");
            throw;
        }
    }

    [HttpPut("{id}")]
    public void Put(string id, [FromBody] Product product)
    {
        try
        {
            _service.Put(id, product);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Put");
            throw;
        }
    }

}

using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _service;



    public ProductController(ILogger<ProductController> logger, ILogger<ProductService> loggerService)
    {
        _service = new ProductService(loggerService);
        _logger = logger;
        try
        {
            var hostName = System.Net.Dns.GetHostName();
            var ips = System.Net.Dns.GetHostAddresses(hostName);
            var _ipaddr = ips.First().MapToIPv4().ToString();
            _logger.LogInformation(1, $"ProductService responding from {_ipaddr}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductController");
        }

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
            _logger.LogError(e, "Error in Get -Lajer");
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
            _logger.LogError(e, "Error in Get by id - boes");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDTO productDTO)
    {
        Product returnProduct = await _service.Post(productDTO);
        if (returnProduct != null)
        {
            return Ok(returnProduct);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromBody] Product product)
    {
        if (_service.Put(product) != null)
        {
            return Ok(product);
        }
        else
        {
            return BadRequest();
        }
    }

}

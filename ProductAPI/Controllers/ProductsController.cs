using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _service;

    public ProductsController(ILogger<ProductsController> logger, IProductService service)
    {
        _service = service;
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
            _logger.LogError(e, "Error in ProductsController");
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

    [HttpPost("by-ids")]
    public async Task<IActionResult> Get([FromBody]List<string> Ids)
    {
        _logger.LogInformation("User is getting products by ids", Ids);
        try
        {
            return Ok(await _service.Get(Ids));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Get -Lajer");
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



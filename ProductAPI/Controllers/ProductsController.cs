using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductAPI.Controllers;

[Authorize]
[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _service;
    private readonly IInfraRepo _infraRepo;

    public ProductsController(ILogger<ProductsController> logger, IProductService service, IInfraRepo infraRepo)
    {
        _service = service;
        _logger = logger;
        _infraRepo = infraRepo;
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

    /// <summary>
    /// Get all products
    /// </summary>
    /// <response code="200">Returns all products</response>
    /// <response code="400">If the request is bad</response>
    /// <response code="401">If the user is not authorized</response>
    /// <response code="404">If no products are found</response>
    [SwaggerResponse(200, "List of products", typeof(List<Product>))]
    [HttpGet]
    public async Task<ActionResult<List<Product>>> Get()
    {
        try
        {
            return await _service.Get();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Get");
            throw;
        }
    }

    /// <summary>
    /// Get a product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns><product></product></returns>
    /// <response code="200">Returns the product with the given id</response>
    /// <response code="400">If the request is bad</response>
    /// <response code="401">If the user is not authorized</response>
    /// <response code="404">If no product is found</response>
    [HttpGet("{id}")]
    public async Task<Product> Get(string id)
    {
        try
        {
            return await _service.Get(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Get by id");
            throw;
        }
    }

    /// <summary>
    /// Get products by ids
    /// </summary>
    /// <param name="Ids"></param>
    /// <response code="200">Returns the products with the given ids</response>
    /// <response code="400">If the request is bad</response>
    /// <response code="401">If the user is not authorized</response>
    /// <response code="404">If no products are found</response>
    [SwaggerResponse(200, "List of products by ids", typeof(List<Product>))]
    [HttpPost("by-ids")]
    public async Task<IActionResult> Get([FromBody] List<string> Ids)
    {
        _logger.LogInformation("User is getting products by ids", Ids);
        try
        {
            return Ok(await _service.Get(Ids));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Get");
            throw;
        }
    }

    /// <summary>
    /// Post a product
    /// </summary>
    /// <param name="productDTO"></param>
    /// <returns><product></product></returns>
    /// <response code="200">Returns the product that was posted</response>
    /// <response code="400">If the request is bad</response>
    /// <response code="401">If the user is not authorized</response>
    /// <response code="404">If no products are found</response>
    /// <response code="500">If the product could not be posted</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDTO productDTO)
    {
        try 
        {
            string JWT = Request.Headers["Authorization"]!;
            _logger.LogInformation("Product posted" + productDTO + " with JWT " + JWT);
            return Ok(await _service.Post(productDTO, JWT));
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Error in Post: ArgumentException");
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Post - possible token is not valid");
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Updates a product
    /// </summary>
    /// <param name="product"></param>
    /// <returns><product></product></returns>
    /// <response code="200">Returns the product that was put</response>
    /// <response code="400">If the request is bad</response>
    /// <response code="401">If the user is not authorized</response>
    /// <response code="404">If no products are found</response>
    /// <response code="500">If the product could not be put</response>
    [HttpPut("")]
    public async Task<ActionResult<Product>> Put([FromBody] Product product)
    {
        _logger.LogInformation("User is putting a product", product);
        try {
            string JWT = Request.Headers["Authorization"]!;
            _logger.LogInformation("Product put", product);
            return Ok(await _service.Put(product, JWT));
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Error in Put: ArgumentException");
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Put - possible token is not valid");
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id"></param>
    /// <returns><product></product></returns>
    /// <response code="200">Returns the product that was deleted</response>
    /// <response code="400">If the request is bad</response>
    /// <response code="401">If the user is not authorized</response>
    /// <response code="404">If no products are found</response>
    /// <response code="500">If the product could not be deleted</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try {
            _logger.LogInformation("Product deleted", id);
            await _service.Delete(id);
            return Ok("Product deleted " + id);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Error in Delete: ArgumentException");
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Delete - possible token is not valid");
            return BadRequest(e.Message);
        }
    }
}



using System.Net;
using ProductAPI.Models;

namespace ProductAPI.Services;


public class ProductService : IProductService
{
    private readonly IInfraRepo _InfraRepo;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;



    public ProductService(IInfraRepo InfraRepo, IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _InfraRepo = InfraRepo;
        _logger = logger;
    }

    public async Task<List<Product>> Get()
    {
        return await _productRepository.Get();
    }

    public async Task<Product> Get(string id)
    {
        return await _productRepository.Get(id);
    }

    public Task<List<Product>> Get(List<string> ids)
    {
        return _productRepository.Get(ids);
    }

    public async Task<Product> Post(ProductDTO productDTO, string token)
    {
        try
        {
            await CheckProductForErrors(productDTO, token);
            return await _productRepository.Post(productDTO);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Error in Post: ArgumentException");
            throw e;
        }
        catch
        {
            _logger.LogError("Error in Post");
            throw;
        }
    }

    public async Task<Product> Put(Product product, string token)
    {
        try
        {
            _logger.LogInformation("User is updating a product", product);
            await CheckProductForErrors(new(product), token);
            return await _productRepository.Put(product);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Error in Put: ArgumentException");
            throw e;
        }
        catch
        {
            _logger.LogError("Error in Put");
            throw;
        }
    }

    public Task<HttpStatusCode> Delete(string id)
    {
        try
        {
            return _productRepository.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task CheckProductForErrors(ProductDTO productDTO, string token)
    {
        _logger.LogInformation("User is posting a product", productDTO);
        try
        {
            if (productDTO == null)
            {
                throw new ArgumentException("Product is null in CheckProductForErrors");
            }
            if (productDTO.SellerId == null)
            {
                throw new ArgumentException("SellerId is null");
            }
            _logger.LogInformation("ProductService | Post | Checking if user exists: " + token);
            if (await _InfraRepo.doesUserExist(productDTO.SellerId, token) != HttpStatusCode.OK)
            {
                throw new ArgumentException("SellerId is not valid, user does not exist or token is invalid ");
            }
            if (productDTO.ProductName == "" || productDTO.ProductName == null)
            {
                throw new ArgumentException("Product name is empty");
            }
            List<Product> products = await _productRepository.Get();
            if (products.Exists(x => x.ProductName == productDTO.ProductName && x.SellerId == productDTO.SellerId && x.Description == productDTO.Description))
            {
                throw new ArgumentException("Product already exists");
            }
            if (productDTO.Valuation < 0)
            {
                _logger.LogError("Valuation is negative");
                throw new ArgumentException("Valuation is negative");
            }
            if (productDTO.Status < 0)
            {
                throw new ArgumentException("Status is negative");
            }
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Error in CheckProductForErrors: ArgumentException");
            throw e;
        }
        catch
        {
            _logger.LogError("Error in CheckProductForErrors");
            throw; 
        }
    }

}

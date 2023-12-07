

using System;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Driver;
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

    public async Task<Product> Post(ProductDTO productDTO)
    {
       Product product = new Product(productDTO);
        try
        {
            _logger.LogInformation("User is posting a product", product);
            if (product == null)
            {
                throw new ArgumentException("Product is null");
            }
            if (product.SellerId == null)
            {
                throw new ArgumentException("SellerId is null");
            }
            /*
            if (await _InfraRepo.verifyUser(product.SellerId) != HttpStatusCode.OK)
            {
                throw new ArgumentException("SellerId is not valid");
            }
            */
            if (product.Valuation < 0)
            {
                _logger.LogError("Valuation is negative");
                throw new ArgumentException("Valuation is negative");
            }
            if (product.Status < 0 || product.Status > 10)
            {
                //throw new Exception("Status is negative");
                throw new ArgumentException("Status is negative");
            }
            await _productRepository.Post(product);
            _logger.LogInformation("Product posted", product);
            return product;
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Error in Post: ArgumentException");
            throw;
        }
        catch
        {
            _logger.LogError("Error in Post");
            throw;
        }
    }

    public async Task<Product> Put(Product product)
    {

        try
        {
            if (product == null)
            {
                throw new ArgumentException("Product is null");
            }
            if (product.SellerId == null)
            {
                throw new ArgumentException("SellerId is null");
            }
            if (await _InfraRepo.doesUserExist(product.SellerId) != HttpStatusCode.OK)
            {
                throw new ArgumentException("SellerId is not valid");
            }
            if (product.Valuation < 0)
            {
                throw new ArgumentException("Valuation is negative");
            }
            if (product.Status < 0 || product.Status > 10)
            {
                //throw new Exception("Status is negative");
                throw new ArgumentException("Status is negative");
            }

            if (await _productRepository.Put(product) != HttpStatusCode.OK)
            {
                throw new ArgumentException("Product not found");            }
            return product;

        }

        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch
        {
            throw;
        }
    }
}

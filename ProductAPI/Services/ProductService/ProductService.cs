

using System;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using ProductAPI.Models;

namespace ProductAPI.Services;


public class ProductService : IProductService
{
    private readonly IAPIService _apiService;
    private readonly IProductRepository _productRepository;

    public ProductService()
    {
        _productRepository = new ProductRepositoryMongo();
        _apiService = new APIService();
    }

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _apiService = new APIService();
    }

    public ProductService(IAPIService apiService, IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _apiService = apiService;
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

    public async Task<HttpStatusCode> Post(Product product)
    {  
        try {
            if (product == null)
            {
                throw new ArgumentException("Product is null");
            }
            if (product.SellerId == null)
            {
                throw new ArgumentException("SellerId is null");
            }
            if (await _apiService.verifyUser(product.SellerId) != HttpStatusCode.OK){
                return HttpStatusCode.BadRequest;
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
            await _productRepository.Post(product);
            return HttpStatusCode.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return HttpStatusCode.BadRequest;
        }catch
        {
            return HttpStatusCode.InternalServerError;
        }
    }

    public async Task<HttpStatusCode> Put(string id, [FromBody] Product product)
    {   
        try {
            if (product == null)
            {
                throw new ArgumentException("Product is null");
            }
            if (product.SellerId == null)
            {
                throw new ArgumentException("SellerId is null");
            }
            if (await _apiService.verifyUser(product.SellerId) != HttpStatusCode.OK){
                return HttpStatusCode.BadRequest;
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
            await _productRepository.Put(id, product);
            return HttpStatusCode.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return HttpStatusCode.BadRequest;
        }catch
        {
            return HttpStatusCode.InternalServerError;
        }
    }
}

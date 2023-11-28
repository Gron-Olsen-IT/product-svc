using System;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using ProductAPI.Models;

namespace ProductAPI.Services;


public class ProductMongo : IProductService
{
    private readonly IMongoCollection<Product> _collection;
    private readonly IAPIService _apiService;

    public ProductMongo(IAPIService apiService)
    {
        //string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "mongodb://admin:1234@localhost:27017/?authMechanism=DEFAULT";
        string connectionString = "mongodb://admin:1234@localhost:27017";
        var mongoDatabase = new MongoClient(connectionString).GetDatabase("ProductDB");

        _collection = mongoDatabase.GetCollection<Product>("products");
        _apiService = apiService;
    }

    public ProductMongo(IMongoCollection<Product> mongoCollection, IAPIService apiService)
    {
        _collection = mongoCollection;
        _apiService = apiService;
    }

    public async Task<List<Product>> Get()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Product> Get(string id)
    {
        throw new NotImplementedException();
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
                throw new ArgumentException(" SellerId is not valid - not found in user database");
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
            await _collection.InsertOneAsync(product);
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

    public Task<HttpStatusCode> Put(string id, [FromBody] Product product)
    {
        throw new NotImplementedException();
    }
}
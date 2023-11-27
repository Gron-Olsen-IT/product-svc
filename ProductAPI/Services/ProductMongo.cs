using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductAPI.Models;

namespace ProductAPI.Services;


public class ProductMongo : IProductService
{
    private readonly IMongoCollection<Product> _collection;

    public ProductMongo(IConfiguration configuration)
    {
        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        var mongoDatabase = new MongoClient(connectionString).GetDatabase("Product");

        _collection = mongoDatabase.GetCollection<Product>("Products");
    }
    public Task<List<Product>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<Product> Get(string id)
    { 
        throw new NotImplementedException();
    }

    public async Task<HttpStatusCode> Post([FromBody] Product product)
    {
        try {
             var result =  _collection.InsertOneAsync(product);
             return HttpStatusCode.OK;
        }
        catch (Exception e) {
            Console.WriteLine(e); 
            return HttpStatusCode.InternalServerError;
        }
    }

    public Task<HttpStatusCode> Put(string id, [FromBody] Product product)
    {
        throw new NotImplementedException();
    }
}
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductAPI.Models;

namespace ProductAPI.Services;

public class ProductRepositoryMongo : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;
   
    public ProductRepositoryMongo()
    {
        //string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "mongodb://admin:1234@localhost:27017/?authMechanism=DEFAULT";
        string connectionString = "mongodb://admin:1234@localhost:27017";
        var mongoDatabase = new MongoClient(connectionString).GetDatabase("ProductDB");

        _collection = mongoDatabase.GetCollection<Product>("products");
        
    }
    
    public ProductRepositoryMongo(IMongoCollection<Product> mongoCollection)
    {
        _collection = mongoCollection;
    }
    public async Task<List<Product>> Get()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public Task<Product> Get(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<HttpStatusCode> Post([FromBody] Product product)
    {
        try {
            await _collection.InsertOneAsync(product);
            return HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return HttpStatusCode.InternalServerError;
        }
    }

    public Task<HttpStatusCode> Put(string id, [FromBody] Product product)
    {
        throw new NotImplementedException();
    }
}

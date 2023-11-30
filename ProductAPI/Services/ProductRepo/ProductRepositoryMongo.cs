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
        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "mongodb://admin:1234@localhost:27017/?authMechanism=DEFAULT";
        //string connectionString = "mongodb://admin:1234@localhost:27017";
        var mongoDatabase = new MongoClient(connectionString).GetDatabase("product_db");

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

    public async Task<Product> Get(string id)
    {
        return await _collection.Find(product => product.Id == id).FirstAsync();
    }

    public Task<List<Product>> Get(List<string> ids)
    {
        return _collection.Find(product => ids.Contains(product.Id!)).ToListAsync();
    }

    public async Task<Product> Post([FromBody] Product product)
    {
        await _collection.InsertOneAsync(product);
        return product;
    }

    public Task<HttpStatusCode> Put([FromBody] Product product)
    {
        try
        {
            _collection.ReplaceOne(x => x.Id == product.Id, product);
            return Task.FromResult(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(HttpStatusCode.InternalServerError);
        }
    }
}

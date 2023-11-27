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

    public ProductMongo()
    {
        //string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "mongodb://admin:1234@localhost:27017/?authMechanism=DEFAULT";
        string connectionString = "mongodb://admin:1234@localhost:27017";
        var mongoDatabase = new MongoClient(connectionString).GetDatabase("ProductDB");

        _collection = mongoDatabase.GetCollection<Product>("products");
    }
    public Task<List<Product>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<Product> Get(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<HttpStatusCode> Post(Product product)
    {
        try {
            await _collection.InsertOneAsync(product);
            return HttpStatusCode.OK;
        }catch (Exception e)
        {
            Console.WriteLine(e);
            return HttpStatusCode.InternalServerError;
        }


        /*
        var result = await _collection.InsertOneAsync(product);
        if (result.IsCompletedSuccessfully)
        {
            return HttpStatusCode.OK;
        }
        else
        {
            return HttpStatusCode.InternalServerError;
        }
        */
    }

    public Task<HttpStatusCode> Put(string id, [FromBody] Product product)
    {
        throw new NotImplementedException();
    }
}
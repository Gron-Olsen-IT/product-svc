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

    public Task<IActionResult> Post([FromBody] Product product)
    {
        return await _collection.InsertOneAsync(product);
    }

    public Task<IActionResult> Put(string id, [FromBody] Product product)
    {
        throw new NotImplementedException();
    }
}
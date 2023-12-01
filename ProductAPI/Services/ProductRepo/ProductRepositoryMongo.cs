using System.Net;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductAPI.Models;

namespace ProductAPI.Services;

public class ProductRepositoryMongo : IProductRepository
{
    private readonly IMongoCollection<Product>? _collection;
    private readonly ILogger<ProductRepositoryMongo> _logger;

    public ProductRepositoryMongo(ILogger<ProductRepositoryMongo> logger)
    {
        _logger = logger;
        try
        {
            var hostName = System.Net.Dns.GetHostName();
            var ips = System.Net.Dns.GetHostAddresses(hostName);
            var _ipaddr = ips.First().MapToIPv4().ToString();
            Console.WriteLine($"ProductService responding from {_ipaddr}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        try {
            string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;
            var mongoDatabase = new MongoClient(connectionString).GetDatabase("product_db");
            _collection = mongoDatabase.GetCollection<Product>("products");
        
        }catch(Exception e){
            _logger.LogError("Something is wrong with the CONNECTION_STRING",  e);
        }
        

    }

    public ProductRepositoryMongo(IMongoCollection<Product> mongoCollection, ILogger<ProductRepositoryMongo> logger)
    {
        _collection = mongoCollection;
        _logger = logger;
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
        await _collection!.InsertOneAsync(product);
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

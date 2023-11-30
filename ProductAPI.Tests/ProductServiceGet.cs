using System.Net;
using Moq;
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Services;
using Microsoft.Extensions.Logging;

namespace ProductAPI.Tests;

public class ProductRepositoryGet
{
    private Mock<IProductRepository> _mockMongoRepository;
    private IProductService _service;
    Mock<ILogger<ProductService>> _mockLogger = new Mock<ILogger<ProductService>>();
    Mock<IInfraRepo> _mockInfraRepo = new Mock<IInfraRepo>();

    List<Product> testData = new List<Product>
            {
                new Product ( "10", 200, DateTime.Now, 1 ),
                new Product ( "3", 389, DateTime.Now, 3 ),
                new Product ( "1", 1000, DateTime.Now, 5 ),
                new Product ( "11", 500, DateTime.Now, 2 ),
            };

    [SetUp]
    public void Setup()
    {
        testData[0].Id = "0";
        testData[1].Id = "1";
        testData[2].Id = "2";
        testData[3].Id = "3";
        
        _mockMongoRepository = new Mock<IProductRepository>();
        
        _mockMongoRepository.Setup(service => service.Get()).ReturnsAsync(testData);
        _mockMongoRepository.Setup(service => service.Get("3")).ReturnsAsync(testData[1]);
        _mockMongoRepository.Setup(service => service.Get(new List<string> { testData[0].Id!, testData[1].Id!, testData[2].Id!, testData[3].Id! })).ReturnsAsync(testData);

        _service = new ProductService(_mockInfraRepo.Object, _mockMongoRepository.Object, _mockLogger.Object);
    }

    [Test]
    public async Task ProductGetAllSuccesful()
    {
        Assert.That((await _service.Get()).Count, Is.EqualTo(4));
    }

    [Test]
    public async Task ProductGetSuccesful() 
    {
        Assert.That((await _service.Get("3")).SellerId, Is.EqualTo("3"));
    }

    [Test]
    public async Task ProductsGetByIdsSuccesful() 
    {
        var products = await _service.Get(new List<string> { testData[0].Id!, testData[1].Id!, testData[2].Id!, testData[3].Id! });
        Assert.That(testData, Is.EqualTo(products));
    }

}
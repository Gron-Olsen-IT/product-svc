using System.Net;
using Moq;
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Tests;

public class ProductRepositoryGet
{
    private Mock<IProductRepository> _mockMongoRepository;
    private IProductService _service;

    [SetUp]
    public void Setup()
    {
        _mockMongoRepository = new Mock<IProductRepository>();
        _service = new ProductService(_mockMongoRepository.Object);
    }

    [Test]
    public async Task ProductGetSuccesful()
    {
        var testData = new List<Product>
            {
                new Product ( "10", 1000, DateTime.Now, 5 ),
                new Product ( "11", 500, DateTime.Now, 2 )
            };
        _mockMongoRepository.Setup(service => service.Get()).ReturnsAsync(testData);
        //Arrange
        List<Product> response = await _service.Get();
        //Act
        Assert.That(response.Count, Is.EqualTo(2));

    }



}
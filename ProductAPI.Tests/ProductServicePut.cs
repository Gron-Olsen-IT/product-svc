using System.Net;
using Moq;
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Services;
using Microsoft.Extensions.Logging;

namespace ProductAPI.Tests;

public class ProductServicePut
{
    private ProductService _service;
    private Mock<IProductRepository> _mockMongoRepository;
    private Mock<IInfraRepo> _mockInfraRepo;

    Product product1 = new Product("1000", 1000, DateTime.Now, 8);

    [SetUp]
    public void Setup()
    {

        Mock<ILogger<ProductService>> _mockLogger = new Mock<ILogger<ProductService>>();
        _mockInfraRepo = new Mock<IInfraRepo>();
        string sellerIdValid = "1000";
        _mockInfraRepo.Setup(service => service.verifyUser(sellerIdValid)).ReturnsAsync(HttpStatusCode.OK);
        product1.Id = "1";
        _mockMongoRepository = new Mock<IProductRepository>();
        _mockMongoRepository.Setup(service => service.Put(product1)).ReturnsAsync(HttpStatusCode.OK);
        _service = new ProductService(_mockInfraRepo.Object, _mockMongoRepository.Object, _mockLogger.Object);
    }





    [Test]
    public async Task ProductUpdatedSuccesfully()
    {
        //Arrange
        
        //Act
        Product responseProduct = await _service.Put(product1);
        Assert.That(responseProduct, Is.EqualTo(product1));

    }




}
using System.Net;
using Moq;
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Tests;

public class ProductServicePut
{
    private ProductService _service;
    private Mock<IProductRepository> _mockMongoRepository;
    private Mock<IAPIService> _mockApiService;

    Product product1 = new Product("1000", 1000, DateTime.Now, 8);

    [SetUp]
    public void Setup()
    {
        _mockApiService = new Mock<IAPIService>();
        string sellerIdValid = "1000";
        _mockApiService.Setup(service => service.verifyUser(sellerIdValid)).ReturnsAsync(HttpStatusCode.OK);
        product1.Id = "1";
        _mockMongoRepository = new Mock<IProductRepository>();
        _mockMongoRepository.Setup(service => service.Put(product1)).ReturnsAsync(HttpStatusCode.OK);
        _service = new ProductService(_mockApiService.Object, _mockMongoRepository.Object);
    }





    [Test]
    public async Task ProductUpdatedSuccesfully()
    {

        //Act
        HttpStatusCode response = await _service.Put(product1);
        Assert.That(response, Is.EqualTo(HttpStatusCode.OK));
    }




}
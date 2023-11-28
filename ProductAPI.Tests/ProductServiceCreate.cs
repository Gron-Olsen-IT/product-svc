using System.Net;
using Moq;
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Tests;

public class ProductRepositoryCreate
{
    private ProductMongo _service;
    private Mock< IMongoCollection<Product> > _mockCollectionProduct;
    private Mock<IAPIService> _mockApiService;

    [SetUp]
    public void Setup()
    {
        _mockCollectionProduct = new Mock< IMongoCollection<Product> >();
        
        _mockApiService = new Mock<IAPIService>();
        string sellerIdValid = "1000";
        _mockApiService.Setup(service => service.verifyUser(sellerIdValid)).ReturnsAsync(HttpStatusCode.OK);

        _service = new ProductMongo(_mockCollectionProduct.Object, _mockApiService.Object);
    }

    [Test]
    public async Task ProductCreateSuccesful()
    {
        //Arrange
        Product product = new Product("1000", 1000, DateTime.Now, 8);
        //Act
        await _service.Post(product);
       
        HttpStatusCode response = await _service.Post(product);
        Assert.That(response, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task ProductCreateFailSellerid()
    {
        //Arrange
        Product product = new Product("500", 1000, DateTime.Now, 5);
        //Act
        await _service.Post(product);
        HttpStatusCode response = await _service.Post(product);
        Assert.That(response, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task ProductCreateFailValuation()
    {
        //Arrange
        Product product = new Product("1000", -540, DateTime.Now, 5);
        //Act
        await _service.Post(product);
        HttpStatusCode response = await _service.Post(product);
        Assert.That(response, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task ProductCreateFailStatus()
    {
        //Arrange
        Product product = new Product("1000", 2000, DateTime.Now, -2);
        //Act
        await _service.Post(product);
        HttpStatusCode response = await _service.Post(product);
        Assert.That(response, Is.EqualTo(HttpStatusCode.BadRequest));
    }



}
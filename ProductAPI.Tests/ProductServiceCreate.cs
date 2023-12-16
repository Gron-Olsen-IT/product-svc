using System.Net;
using Moq;
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Tests;

public class ProductRepositoryCreate
{
    private ProductService _service;
    private Mock<IProductRepository> _mockMongoRepository;
    private Mock<IInfraRepo> _mockInfraRepo;
    private string jwtTokenValid;

    private Mock<ILogger<ProductService>> _mockLogger;

    ProductDTO productDTO = new ProductDTO("1000","Taske", "Dejlig taske", 1000, DateTime.Now, ProductStatus.Active);

    [SetUp]
    public void Setup()
    {
        _mockMongoRepository = new Mock<IProductRepository>();
        _mockLogger = new Mock<ILogger<ProductService>>();
        
        _mockInfraRepo = new Mock<IInfraRepo>();
        string sellerIdValid = "1000";
        jwtTokenValid = "okmasoimj435oi2m34ip56oj1345+0i2+103291rmk21k0r+1k3+0rj1ri351ontoin542n";
        _mockInfraRepo.Setup(service => service.doesUserExist(sellerIdValid, jwtTokenValid)).ReturnsAsync(HttpStatusCode.OK);
        _mockMongoRepository.Setup(service => service.Get()).ReturnsAsync(new List<Product>());
        _mockMongoRepository.Setup(service => service.Post(productDTO)).ReturnsAsync(new Product(productDTO));

        _service = new ProductService(_mockInfraRepo.Object, _mockMongoRepository.Object, _mockLogger.Object);
    }

    [Test]
    public async Task ProductCreateSuccesful()
    {
        //Arrange
        
        //Act
        await _service.Post(productDTO, jwtTokenValid);

        Product responseProduct = await _service.Post(productDTO, jwtTokenValid);
        ProductDTO responseDTO = new ProductDTO(responseProduct.SellerId, responseProduct.ProductName, responseProduct.Description, responseProduct.Valuation, responseProduct.CreateAt, responseProduct.Status);
        Assert.That(responseDTO, Is.EqualTo(productDTO));
    }

    /*
    [Test]
    public async Task ProductCreateFailSellerId()
    {


        await Task.Run(() =>
        {
            //Arrange
            ProductDTO productDTO = new ProductDTO("500", 1000, DateTime.Now, 5);
            //Act
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.Post(productDTO));
        });
    }
    */

    [Test]
    public async Task ProductCreateFailValuation()
    {
        await Task.Run(() =>
        {
            //Arrange
            ProductDTO productDTO = new ProductDTO("1000", "Bil", "Smart bil", -10, DateTime.Now, ProductStatus.Active);
            //Act
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.Post(productDTO, jwtTokenValid));
        });
    }

    [Test]
    public async Task ProductCreateFailStatus()
    {
        await Task.Run(() =>
        {
            //Arrange
            ProductDTO productDTO = new ProductDTO("1000", "Bil", "Smart bil", -10, DateTime.Now, ProductStatus.Active);
            //Act
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.Post(productDTO, jwtTokenValid));
        });
    }


}
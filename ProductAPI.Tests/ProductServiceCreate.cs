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

    private Mock<ILogger<ProductService>> _mockLogger;



    [SetUp]
    public void Setup()
    {
        _mockMongoRepository = new Mock<IProductRepository>();
        _mockLogger = new Mock<ILogger<ProductService>>();
        
        _mockInfraRepo = new Mock<IInfraRepo>();
        string sellerIdValid = "1000";
        _mockInfraRepo.Setup(service => service.doesUserExist(sellerIdValid)).ReturnsAsync(HttpStatusCode.OK);

        _service = new ProductService(_mockInfraRepo.Object, _mockMongoRepository.Object, _mockLogger.Object);
    }

    [Test]
    public async Task ProductCreateSuccesful()
    {
        //Arrange
        ProductDTO productDTO = new ProductDTO("1000", 1000, DateTime.Now, 8);
        //Act
        await _service.Post(productDTO);

        Product responseProduct = await _service.Post(productDTO);
        ProductDTO responseDTO = new ProductDTO(responseProduct.SellerId, responseProduct.Valuation, responseProduct.CreateAt, responseProduct.Status);
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
            ProductDTO productDTO = new ProductDTO("1000", -10, DateTime.Now, 5);
            //Act
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.Post(productDTO));
        });
    }

    [Test]
    public async Task ProductCreateFailStatus()
    {
        await Task.Run(() =>
        {
            //Arrange
            ProductDTO productDTO = new ProductDTO("1000", 2000, DateTime.Now, -2);
            //Act
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.Post(productDTO));
        });
    }


}
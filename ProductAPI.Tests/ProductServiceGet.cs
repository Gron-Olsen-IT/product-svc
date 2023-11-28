using System.Net;
using Moq;
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Tests;

public class ProductRepositoryGet
{
    private ProductMongo _service;
    private Mock<IMongoCollection<Product>> _mockCollectionProduct;
    //private Mock<IAPIService> _mockApiService;

    [SetUp]
    public void Setup()
    {
        _mockCollectionProduct = new Mock<IMongoCollection<Product>>();
        var testData = new List<Product>
            {
                new Product ( "10", 1000, DateTime.Now, 5 ),
                new Product ( "11", 500, DateTime.Now, 2 )
            };
        // Setup the Find method to return the test data
        //_mockCollectionProduct.Setup(x=> x.InsertMany(testData, null, default));
        _service = new ProductMongo(_mockCollectionProduct.Object, new APIService());
        _service.Post(new Product ( "10", 1000, DateTime.Now, 5 ));
        _service.Post(new Product ( "11", 500, DateTime.Now, 2 ));

        
    }

    [Test]
    public async Task ProductGetSuccesful()
    {
        //Arrange
        List<Product> response = await _service.Get();
        //Act
        Assert.That(response, Is.Not.Null);

    }



}
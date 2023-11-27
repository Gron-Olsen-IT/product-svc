using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Moq;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Tests;

public class Tests
{
    private ProductMongo _service = new ProductMongo();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task ProductCreateSuccesful()
    {
        Product product = new Product();
        product.SellerId = "Test";
        product.Valuation = 1000;
        HttpStatusCode response = await _service.Post(new Product());
        //Assert.AreEqual(HttpStatusCode.Created, response);
        Assert.That(response, Is.EqualTo(HttpStatusCode.OK));
    }
}
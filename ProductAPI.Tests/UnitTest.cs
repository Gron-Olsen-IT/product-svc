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
        Product product = new Product("10", 1000, DateTime.Now, 100);
        HttpStatusCode response = await _service.Post(product);
        //Assert.AreEqual(HttpStatusCode.Created, response);
        Assert.That(response, Is.EqualTo(HttpStatusCode.OK));
    }
}
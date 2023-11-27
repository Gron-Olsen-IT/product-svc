namespace ProductAPI.Tests;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using ProductAPI.Models;
using ProductAPI.Services;

public class Tests
{
    private readonly ProductMongo service = new ProductMongo();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task ProductCreateSuccesful()
    {
        IActionResult response = await service.Post(new Product());
        Assert.IsInstanceOf<OkResult>(response);
    }
}
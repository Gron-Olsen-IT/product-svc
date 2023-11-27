namespace ProductAPI.Tests;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

public class Tests
{
    private readonly ProductService productService = new ProductService();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task ProductCreateSuccesful()
    {
        IActionResult response = await productService.Post(new Product());
        Assert.IsInstanceOf<OkResult>(response);
    }
}
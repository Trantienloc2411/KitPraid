using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProductService.Api.Controllers;
using ProductService.Application.Dtos;
using ProductService.Application.Services;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Api.Test.Controllers;

[TestFixture]
public class ProductControllerTests
{
    private Mock<IProductService> _service = null!;
    private Mock<ILogger<ProductController>> _logger = null!;
    private ProductController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _service = new Mock<IProductService>();
        _logger = new Mock<ILogger<ProductController>>();
        _controller = new ProductController(_service.Object, _logger.Object);
    }

    [Test]
    public async Task Get_ShouldReturnOk_WhenServiceSucceeds()
    {
        var request = new PageRequestDto { Page = 1, Size = 10 };
        var items = new List<GetProductDto> { new GetProductDto { ProductName = "P1", Sku = "S1", Stock = 1, Price = 10 } };
        var page = new PageResult<GetProductDto>(items, totalCount: 1, page: 1, size: 10);
        var expected = OperationResult<PageResult<GetProductDto>>.Ok(page);

        _service.Setup(s => s.GetAllProductsAsync(It.IsAny<PageRequest>())).ReturnsAsync(expected);

        var action = await _controller.Get(request);

        action.Result.Should().BeOfType<OkObjectResult>();
        var ok = action.Result as OkObjectResult;
        ok!.Value.Should().Be(expected);
    }

    [Test]
    public async Task Get_ShouldReturnBadRequest_WhenServiceFails()
    {
        var request = new PageRequestDto { Page = 1, Size = 10 };
        var expected = OperationResult<PageResult<GetProductDto>>.Fail("err");

        _service.Setup(s => s.GetAllProductsAsync(It.IsAny<PageRequest>())).ReturnsAsync(expected);

        var action = await _controller.Get(request);

        action.Result.Should().BeOfType<BadRequestObjectResult>();
        var bad = action.Result as BadRequestObjectResult;
        bad!.Value.Should().Be(expected);
    }

    [Test]
    public async Task GetWithKeyWord_ShouldReturnOk_WhenServiceSucceeds()
    {
        var keyword = "key";
        var request = new PageRequestDto { Page = 1, Size = 5 };
        var items = new List<GetProductDto>();
        var page = new PageResult<GetProductDto>(items, 0, 1, 5);
        var expected = OperationResult<PageResult<GetProductDto>>.Ok(page);

        _service.Setup(s => s.GetProductsByKeywordAsync(It.IsAny<PageRequest>(), keyword)).ReturnsAsync(expected);

        var action = await _controller.GetWithKeyWord(keyword, request);

        action.Result.Should().BeOfType<OkObjectResult>();
        (action.Result as OkObjectResult)!.Value.Should().Be(expected);
    }

    [Test]
    public async Task GetWithKeyWord_ShouldReturnBadRequest_WhenServiceFails()
    {
        var keyword = "key";
        var request = new PageRequestDto { Page = 1, Size = 5 };
        var expected = OperationResult<PageResult<GetProductDto>>.Fail("err");

        _service.Setup(s => s.GetProductsByKeywordAsync(It.IsAny<PageRequest>(), keyword)).ReturnsAsync(expected);

        var action = await _controller.GetWithKeyWord(keyword, request);

        action.Result.Should().BeOfType<BadRequestObjectResult>();
        (action.Result as BadRequestObjectResult)!.Value.Should().Be(expected);
    }

    [Test]
    public async Task Create_ShouldReturnOk_WhenServiceSucceeds()
    {
        var dto = new CreateProductDto { ProductName = "Name", ProductDescription = "Description long enough", Price = 10, BrandId = Guid.NewGuid(), Sku = "SKU1", Stock = 1, Attributes = new Dictionary<string, object>() };
        var created = new Product { ProductName = "Name", ProductDescription = "Description long enough", Sku = "SKU1", Stock = 1, BrandId = Guid.NewGuid(), Price = 10 };
        var expected = OperationResult<Product>.Ok(created);

        _service.Setup(s => s.CreateProductAsync(dto)).ReturnsAsync(expected);

        var action = await _controller.Create(dto);

        action.Result.Should().BeOfType<OkObjectResult>();
        (action.Result as OkObjectResult)!.Value.Should().Be(expected);
    }

    [Test]
    public async Task Create_ShouldReturnBadRequest_WhenServiceFails()
    {
        var dto = new CreateProductDto();
        var expected = OperationResult<Product>.Fail("err");

        _service.Setup(s => s.CreateProductAsync(dto)).ReturnsAsync(expected);

        var action = await _controller.Create(dto);

        action.Result.Should().BeOfType<BadRequestObjectResult>();
        (action.Result as BadRequestObjectResult)!.Value.Should().Be(expected);
    }

    [Test]
    public async Task Update_ShouldReturnOk_WhenServiceSucceeds()
    {
        var id = Guid.NewGuid().ToString();
        var dto = new UpdateProductDto { ProductName = "New" };
        var updated = new Product { ProductName = "New", ProductDescription = "desc", Sku = "SKU", Stock = 1, BrandId = Guid.NewGuid(), Price = 5 };
        var expected = OperationResult<Product>.Ok(updated);

        _service.Setup(s => s.UpdateProductAsync(id, dto)).ReturnsAsync(expected);

        var action = await _controller.Update(id, dto);

        action.Result.Should().BeOfType<OkObjectResult>();
        (action.Result as OkObjectResult)!.Value.Should().Be(expected);
    }

    [Test]
    public async Task Update_ShouldReturnBadRequest_WhenServiceFails()
    {
        var id = Guid.NewGuid().ToString();
        var dto = new UpdateProductDto();
        var expected = OperationResult<Product>.Fail("err");

        _service.Setup(s => s.UpdateProductAsync(id, dto)).ReturnsAsync(expected);

        var action = await _controller.Update(id, dto);

        action.Result.Should().BeOfType<BadRequestObjectResult>();
        (action.Result as BadRequestObjectResult)!.Value.Should().Be(expected);
    }

    [Test]
    public async Task Delete_ShouldReturnOk_WhenServiceSucceeds()
    {
        var id = Guid.NewGuid().ToString();
        var expected = OperationResult<bool>.Ok(true);

        _service.Setup(s => s.DeleteProductAsync(id)).ReturnsAsync(expected);

        var action = await _controller.Delete(id);

        action.Result.Should().BeOfType<OkObjectResult>();
        (action.Result as OkObjectResult)!.Value.Should().Be(expected);
    }

    [Test]
    public async Task Delete_ShouldReturnBadRequest_WhenServiceFails()
    {
        var id = Guid.NewGuid().ToString();
        var expected = OperationResult<bool>.Fail("err");

        _service.Setup(s => s.DeleteProductAsync(id)).ReturnsAsync(expected);

        var action = await _controller.Delete(id);

        action.Result.Should().BeOfType<BadRequestObjectResult>();
        (action.Result as BadRequestObjectResult)!.Value.Should().Be(expected);
    }
}

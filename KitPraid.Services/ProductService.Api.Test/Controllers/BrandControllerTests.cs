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
using ProductService.Application.Services.Interfaces;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;

namespace ProductService.Api.Test.Controllers
{
    [TestFixture]
    public class BrandControllerTests
    {
        private Mock<IBrandService> _service = null!;
        private Mock<ILogger<BrandController>> _logger = null!;
        private BrandController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IBrandService>();
            _logger = new Mock<ILogger<BrandController>>();
            _controller = new BrandController(_service.Object, _logger.Object);
        }

        [Test]
        public async Task GetAllBrandsAsync_ShouldReturnOk_WithPageResult()
        {
            var dto = new PageRequestDto { Page = 1, Size = 10 };
            var items = new List<GetBrandDto> { new GetBrandDto { BrandCode = "C1", BrandName = "ASUS" } };
            var page = new PageResult<GetBrandDto>(items, totalCount: 1, page: 1, size: 10);
            var expected = OperationResult<PageResult<GetBrandDto>>.Ok(page);

            _service.Setup(s => s.GetAllBrandsAsync(It.IsAny<PageRequest>())).ReturnsAsync(expected);

            var result = await _controller.GetAllBrandsAsync(dto);

            result.Result.Should().BeOfType<OkObjectResult>();
            var ok = result.Result as OkObjectResult;
            ok!.Value.Should().Be(expected);

            _service.Verify(s => s.GetAllBrandsAsync(It.IsAny<PageRequest>()), Times.Once);
        }

        [Test]
        public async Task GetBrandAsync_ShouldReturnOk_WithBrand()
        {
            var id = Guid.NewGuid();
            var brand = new GetBrandDto { Id = id, BrandCode = "C1", BrandName = "ASUS" };
            var expected = OperationResult<GetBrandDto>.Ok(brand);

            _service.Setup(s => s.GetBrandAsync(id)).ReturnsAsync(expected);

            var result = await _controller.GetBrandAsync(id);

            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult)!.Value.Should().Be(expected);
        }

        [Test]
        public async Task GetAllItemBrandsAsync_ShouldReturnOk_WithPageResult()
        {
            var id = Guid.NewGuid();
            var request = new PageRequestDto { Page = 1, Size = 5 };
            var expected = OperationResult<PageResult<GetBrandItemsDto>>.Ok(new PageResult<GetBrandItemsDto>(new List<GetBrandItemsDto>(), 0, 1, 5));

            _service.Setup(s => s.GetAllItemBrandsAsync(It.IsAny<PageRequest>(), id)).ReturnsAsync(expected);

            var result = await _controller.GetAllItemBrandsAsync(id, request);

            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult)!.Value.Should().Be(expected);
        }

        [Test]
        public async Task CreateBrandAsync_ShouldReturnOk_WhenServiceSucceeds()
        {
            var dto = new CreateBrandDto { BrandName = "ASUS" };
            var created = new Brand { BrandCode = "CODE1", BrandName = "ASUS", BrandDescription = "d", BrandImage = "i" };
            var expected = OperationResult<Brand>.Ok(created);

            _service.Setup(s => s.CreateBrandAsync(dto)).ReturnsAsync(expected);

            var result = await _controller.CreateBrandAsync(dto);

            var ok = result.Result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().Be(expected);
        }

        [Test]
        public async Task CreateBrandAsync_ShouldReturnBadRequest_WhenServiceFails()
        {
            var dto = new CreateBrandDto();
            var expected = OperationResult<Brand>.Fail("Error!!!");

            _service.Setup(s => s.CreateBrandAsync(dto)).ReturnsAsync(expected);

            var result = await _controller.CreateBrandAsync(dto);

            var bad = result.Result as BadRequestObjectResult;
            bad.Should().NotBeNull();
            bad!.Value.Should().Be(expected);
        }

        [Test]
        public async Task UpdateBrandAsync_ShouldReturnMappedDto_WhenServiceSucceeds()
        {
            var id = Guid.NewGuid();
            var requestDto = new UpdateBrandDto
            {
                BrandName = "Updated",
                Description = "Updated Desc",
                BrandImageUrl = "updated.png",
                IsActive = true
            };

            // Service returns a Brand entity; controller maps Brand -> UpdateBrandDto
            var serviceBrand = new Brand
            {
                BrandCode = "SVC1",
                BrandName = "ServiceName",
                BrandDescription = "Service Desc",
                BrandImage = "https://example.com/service.png",
                IsActive = true
            };

            var serviceResult = OperationResult<Brand>.Ok(serviceBrand);
            _service.Setup(s => s.UpdateBrandAsync(id.ToString(), It.IsAny<UpdateBrandDto>())).ReturnsAsync(serviceResult);

            var result = await _controller.UpdateBrandAsync(id, requestDto);

            result.Result.Should().BeOfType<OkObjectResult>();
            var ok = result.Result as OkObjectResult;

            ok.Should().NotBeNull();

            var value = ok!.Value!;
            var successProp = value.GetType().GetProperty("Success");
            var dataProp = value.GetType().GetProperty("Data");
            successProp.Should().NotBeNull();
            dataProp.Should().NotBeNull();

            var success = (bool?)successProp!.GetValue(value);
            success.Should().BeTrue();

            var dataObj = dataProp!.GetValue(value);
            dataObj.Should().NotBeNull();

            // Data may be of the expected DTO type; use dynamic cast
            var dto = dataObj as UpdateBrandDto;
            dto.Should().NotBeNull();
            dto!.BrandName.Should().Be(serviceBrand.BrandName);
            dto.Description.Should().Be(serviceBrand.BrandDescription);
            dto.BrandImageUrl.Should().Be(serviceBrand.BrandImage);
            dto.IsActive.Should().Be(serviceBrand.IsActive);

            _service.Verify(s => s.UpdateBrandAsync(id.ToString(), It.IsAny<UpdateBrandDto>()), Times.Once);
        }

        [Test]
        public async Task UpdateBrandAsync_ShouldReturnBadRequest_WhenServiceFails()
        {
            var id = Guid.NewGuid();
            var dto = new UpdateBrandDto();
            var expected = OperationResult<Brand>.Fail("Error!");

            _service.Setup(s => s.UpdateBrandAsync(id.ToString(), It.IsAny<UpdateBrandDto>())).ReturnsAsync(expected);

            var result = await _controller.UpdateBrandAsync(id, dto);

            var bad = result.Result as BadRequestObjectResult;
            bad.Should().NotBeNull();
            bad!.Value.Should().Be(expected);
        }

        [Test]
        public async Task DeleteBrandAsync_ShouldReturnOk_WhenServiceSucceeds()
        {
            var id = Guid.NewGuid();
            var expected = OperationResult<Brand>.Ok(new Brand
            {
                BrandCode = "DEL1",
                BrandName = "ToDelete",
                BrandDescription = "desc",
                BrandImage = "https://example.com/img.png",
                IsActive = false
            });

            _service.Setup(s => s.DeleteBrandAsync(id)).ReturnsAsync(expected);

            var result = await _controller.DeleteBrandAsync(id);

            var ok = result.Result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().Be(expected);
        }

        [Test]
        public async Task DeleteBrandAsync_ShouldReturnBadRequest_WhenServiceFails()
        {
            var id = Guid.NewGuid();
            var expected = OperationResult<Brand>.Fail("Delete Fail");

            _service.Setup(s => s.DeleteBrandAsync(id)).ReturnsAsync(expected);

            var result = await _controller.DeleteBrandAsync(id);

            var bad = result.Result as BadRequestObjectResult;
            bad.Should().NotBeNull();
            bad!.Value.Should().Be(expected);
        }
    }
}
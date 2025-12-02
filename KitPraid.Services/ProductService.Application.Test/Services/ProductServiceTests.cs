using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductService.Application.Dtos;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;

namespace ProductService.Application.Test.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _repo;
        private Application.Services.ProductService _service;

        [SetUp]
        public void Setup()
        {
            _repo = new Mock<IProductRepository>();
            _service = new Application.Services.ProductService(_repo.Object);
        }

        // ==========================
        // CreateProductAsync Tests
        // ==========================

        [Test]
        public async Task CreateProductAsync_ShouldFail_WhenSkuExists()
        {
            // Arrange
            var dto = new CreateProductDto { Sku = "SKU1" };

            _repo.Setup(r => r.GetProductBySkuAsync("SKU1"))
                .ReturnsAsync(OperationResult<Product>.Ok(new Product
                {
                    ProductName = "KDT", ProductDescription = "Sir maxcdn", Sku = "please don't use this sku"
                }));

            // Act
            var result = await _service.CreateProductAsync(dto);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("Product already exist");
        }

        [Test]
        public async Task CreateProductAsync_ShouldFail_WhenRepositoryError()
        {
            var dto = new CreateProductDto { Sku = "SKUERR" };

            _repo.Setup(r => r.GetProductBySkuAsync(dto.Sku))
                .ReturnsAsync(OperationResult<Product>.Fail("Database error"));

            var result = await _service.CreateProductAsync(dto);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Database error");
        }

        [Test]
        public async Task CreateProductAsync_ShouldCreateProduct()
        {
            var dto = new CreateProductDto { Sku = "SKU123", ProductName = "Keyboard" };

            // PRODUCT DOES NOT EXIST → Success = false, Error = null
            _repo.Setup(r => r.GetProductBySkuAsync(dto.Sku))
                .ReturnsAsync(new OperationResult<Product>
                {
                    Success = false,
                    Error = null,
                    Data = null
                });

            _repo.Setup(r => r.AddProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(OperationResult<Product>.Ok(new Product
                {
                    Sku = "SKU123",
                    ProductName = "Keyboard",
                    ProductDescription = "Mo's keyboard"
                }));

            var result = await _service.CreateProductAsync(dto);

            result.Success.Should().BeTrue();
            result.Data!.Sku.Should().Be("SKU123");
        }



        [Test]
        public async Task CreateProductAsync_ShouldCatch_DbUpdateException()
        {
            var dto = new CreateProductDto { Sku = "ERR" };

            // SKU does NOT exist and NO error => allow proceeding to AddProductAsync
            _repo.Setup(r => r.GetProductBySkuAsync(dto.Sku))
                .ReturnsAsync(new OperationResult<Product>
                {
                    Success = false,
                    Data = null,
                    Error = null
                });

            // Throw DB exception during AddProductAsync
            _repo.Setup(r => r.AddProductAsync(It.IsAny<Product>()))
                .ThrowsAsync(new DbUpdateException("DB ERROR"));

            var result = await _service.CreateProductAsync(dto);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("DB ERROR");
        }
        
        [Test]
        public async Task CreateProductAsync_ShouldCatch_InvalidOperationException()
        {
            var dto = new CreateProductDto { Sku = "INV-ERR" };

            // SKU does not exist → proceed to AddProductAsync
            _repo.Setup(r => r.GetProductBySkuAsync(dto.Sku))
                .ReturnsAsync(new OperationResult<Product>
                {
                    Success = false,
                    Error = null,
                    Data = null
                });

            _repo.Setup(r => r.AddProductAsync(It.IsAny<Product>()))
                .ThrowsAsync(new InvalidOperationException("Invalid op"));

            var result = await _service.CreateProductAsync(dto);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Details : Invalid op");
        }
        
        [Test]
        public async Task CreateProductAsync_ShouldCatch_ArgumentException()
        {
            var dto = new CreateProductDto { Sku = "ARG-ERR" };

            _repo.Setup(r => r.GetProductBySkuAsync(dto.Sku))
                .ReturnsAsync(new OperationResult<Product>
                {
                    Success = false,
                    Error = null,
                    Data = null
                });

            _repo.Setup(r => r.AddProductAsync(It.IsAny<Product>()))
                .ThrowsAsync(new ArgumentException("Bad argument"));

            var result = await _service.CreateProductAsync(dto);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Details : Bad argument");
        }
        
        [Test]
        public async Task CreateProductAsync_ShouldCatch_GenericException()
        {
            var dto = new CreateProductDto { Sku = "GEN-ERR" };

            _repo.Setup(r => r.GetProductBySkuAsync(dto.Sku))
                .ReturnsAsync(new OperationResult<Product>
                {
                    Success = false,
                    Error = null,
                    Data = null
                });

            _repo.Setup(r => r.AddProductAsync(It.IsAny<Product>()))
                .ThrowsAsync(new Exception("Unknown explode"));

            var result = await _service.CreateProductAsync(dto);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Some unexpected error. Details : Unknown explode");
        }





        // ==========================
        // DeleteProductAsync Tests
        // ==========================

        [Test]
        public async Task DeleteProductAsync_ShouldReturnSuccess()
        {
            _repo.Setup(r => r.DeleteAsync("1"))
                .ReturnsAsync(OperationResult<bool>.Ok(true));

            var result = await _service.DeleteProductAsync("1");

            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();
        }

        [Test]
        public async Task DeleteProductAsync_ShouldCatchException()
        {
            _repo.Setup(r => r.DeleteAsync("1"))
                .ThrowsAsync(new Exception("Delete failed"));

            var result = await _service.DeleteProductAsync("1");

            result.Success.Should().BeFalse();
            result.Error.Should().Contain("Delete failed");
        }

        // ==========================
        // GetAllProductsAsync Tests
        // ==========================

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnPagedProducts()
        {
            var products = new List<Product> 
            { 
                new Product
                {
                    ProductName = "Mouse",
                    Sku = "S1",
                    ProductDescription = "very nice mouse",
                    Images = new List<Image> { new Image { ImageName = "img1", ImagePath = "path1.jpg" } } 
                }
            };

            var pageData = new PageResult<Product>(products, 1, 1, 10);

            _repo.Setup(r => r.GetAllAsync(It.IsAny<PageRequest>()))
                .ReturnsAsync(OperationResult<PageResult<Product>>.Ok(pageData));

            var result = await _service.GetAllProductsAsync(new PageRequest(1, 10));

            result.Success.Should().BeTrue();
            result.Data!.Items.Should().HaveCount(1);
            result.Data.Items.First().Images.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldCatchException()
        {
            _repo.Setup(r => r.GetAllAsync(It.IsAny<PageRequest>()))
                .ThrowsAsync(new Exception("Fetch error"));

            var result = await _service.GetAllProductsAsync(new PageRequest(1, 10));

            result.Success.Should().BeFalse();
            result.Error.Should().Contain("Fetch error");
        }

        // ==========================
        // GetProductsByKeywordAsync Tests
        // ==========================

        [Test]
        public async Task GetProductsByKeywordAsync_ShouldReturnMappedResults()
        {
            var products = new List<Product> { new Product
                {
                    ProductName = "Keyboard",
                    Sku = "SK99",
                    ProductDescription = "Audlent keyboard",
                    Images = new List<Image> { new Image { ImageName = "IMG1", ImagePath = "PATH1.jpg" } }
                }
            };
            var pageData = new PageResult<Product>(products, 1, 1, 10);

            _repo.Setup(r => r.GetAllProductsWithKeywordAsync(It.IsAny<PageRequest>(), "key"))
                .ReturnsAsync(OperationResult<PageResult<Product>>.Ok(pageData));

            var result = await _service.GetProductsByKeywordAsync(new PageRequest(1, 10), "key");

            result.Success.Should().BeTrue();
            result.Data!.Items.Should().HaveCount(1);
        }

        [Test]
        public async Task GetProductsByKeywordAsync_ShouldFail_OnRepositoryError()
        {
            _repo.Setup(r => r.GetAllProductsWithKeywordAsync(It.IsAny<PageRequest>(), "key"))
                .ReturnsAsync(OperationResult<PageResult<Product>>.Fail("search failed"));

            var result = await _service.GetProductsByKeywordAsync(new PageRequest(1, 10), "key");

            result.Success.Should().BeFalse();
            result.Error.Should().Be("search failed");
        }

        [Test]
        public async Task GetProductsByKeywordAsync_ShouldCatch_DbUpdateException()
        {
            _repo.Setup(r => r.GetAllProductsWithKeywordAsync(It.IsAny<PageRequest>(), "key"))
                .ThrowsAsync(new DbUpdateException("DB Search Error"));

            var result = await _service.GetProductsByKeywordAsync(new PageRequest(1, 10), "key");

            result.Success.Should().BeFalse();
            result.Error.Should().Contain("DB Search Error");
        }

        // ==========================
        // GetProductByIdAsync Tests
        // ==========================

        [Test]
        public async Task GetProductByIdAsync_ShouldReturnProduct()
        {
            string id = Guid.NewGuid().ToString();
            var product = new Product
            {
                Id = Guid.Parse(id),
                ProductName = "Keyboard",
                ProductDescription = "vast keyboard",
                Sku = "A002",
                Images = new List<Image> { new Image { ImageName = "img1.jpg", ImagePath = "/img1.jpg" } },
                Brand = new Brand
                {
                    Id = Guid.NewGuid(),
                    BrandName = "BrandX",
                    BrandCode = "BX01",
                    BrandDescription = null,
                    BrandImage = null
                }
            };

            _repo.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(OperationResult<Product>.Ok(product));

            var result = await _service.GetProductByIdAsync(id);

            result.Success.Should().BeTrue();
            result.Data!.ProductName.Should().Be("Keyboard");
            result.Data.Images.Should().HaveCount(1);
            result.Data.BrandDto!.BrandName.Should().Be("BrandX");
        }


        [Test]
        public async Task GetProductByIdAsync_ShouldReturnNotFound()
        {
            _repo.Setup(r => r.GetByIdAsync("1"))
                .ReturnsAsync(OperationResult<Product>.Ok(null!));

            var result = await _service.GetProductByIdAsync("1");

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Product not found");
        }

        [Test]
        public async Task GetProductByIdAsync_ShouldFail_OnRepoError()
        {
            _repo.Setup(r => r.GetByIdAsync("1"))
                .ReturnsAsync(OperationResult<Product>.Fail("DB error"));

            var result = await _service.GetProductByIdAsync("1");

            result.Success.Should().BeFalse();
            result.Error.Should().Be("DB error");
        }

        // ==========================
        // UpdateProductAsync Tests
        // ==========================

        [Test]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            var product = new Product
            {
                ProductName = "OldName",
                ProductDescription = "vast keyboard",
                Sku = "C009"
            };
            var dto = new UpdateProductDto { ProductName = "NewName" };

            _repo.Setup(r => r.GetByIdAsync("1"))
                .ReturnsAsync(OperationResult<Product>.Ok(product));

            _repo.Setup(r => r.UpdateAsync(product))
                .ReturnsAsync(OperationResult<Product>.Ok(product));

            var result = await _service.UpdateProductAsync("1", dto);

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task UpdateProductAsync_ShouldFail_WhenProductNotFound()
        {
            _repo.Setup(r => r.GetByIdAsync("1"))
                .ReturnsAsync(OperationResult<Product>.Ok(null!));

            var result = await _service.UpdateProductAsync("1", new UpdateProductDto());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Product not found");
        }

        [Test]
        public async Task UpdateProductAsync_ShouldFail_OnRepositoryError()
        {
            _repo.Setup(r => r.GetByIdAsync("1"))
                .ReturnsAsync(OperationResult<Product>.Fail("DB Error"));

            var result = await _service.UpdateProductAsync("1", new UpdateProductDto());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("DB Error");
        }
    }
}
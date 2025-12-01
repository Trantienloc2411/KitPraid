using FluentAssertions;
using Moq;
using ProductService.Application.Dtos;
using ProductService.Application.Services;
using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Domain.ValueObjects;

namespace ProductService.Application.Test.Services
{
    [TestFixture]
    public class BrandServiceTests
    {
        private Mock<IBrandRepository> _repo = null!;
        private BrandService _service = null!;

        [SetUp]
        public void Setup()
        {
            _repo = new Mock<IBrandRepository>();
            _service = new BrandService(_repo.Object);
        }

        // ----------------------------------------------------------------------
        // GetAllBrandsAsync
        // ----------------------------------------------------------------------
        [Test]
        public async Task GetAllBrandsAsync_ShouldReturnOk_WhenSuccess()
        {
            var data = new PageResult<Brand>(
                new List<Brand> { new()
                    {
                        BrandName = "Test",
                        BrandCode = "CODE",
                        BrandDescription = "DESP",
                        BrandImage = "noimage.jpg"
                    }
                },
                1, 1, 10
            );

            _repo.Setup(r => r.GetAllBrandsAsync(It.IsAny<PageRequest>()))
                .ReturnsAsync(OperationResult<PageResult<Brand>>.Ok(data));

            var result = await _service.GetAllBrandsAsync(new PageRequest(1, 10));

            result.Success.Should().BeTrue();
            result.Data!.Items.Should().HaveCount(1);
        }

        [Test]
        public async Task GetAllBrandsAsync_ShouldFail_WhenRepositoryFail()
        {
            _repo.Setup(r => r.GetAllBrandsAsync(It.IsAny<PageRequest>()))
                .ReturnsAsync(OperationResult<PageResult<Brand>>.Fail("Err"));

            var result = await _service.GetAllBrandsAsync(new PageRequest(1, 10));

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Err");
        }

        [Test]
        public async Task GetAllBrandsAsync_ShouldFail_WhenNoData()
        {
            var empty = new PageResult<Brand>(new List<Brand>(), 0, 1, 10);

            _repo.Setup(r => r.GetAllBrandsAsync(It.IsAny<PageRequest>()))
                .ReturnsAsync(OperationResult<PageResult<Brand>>.Ok(empty));

            var result = await _service.GetAllBrandsAsync(new PageRequest(1, 10));

            result.Success.Should().BeFalse();
            result.Error.Should().Be("No brands found");
        }

        [Test]
        public async Task GetAllBrandsAsync_ShouldFail_OnException()
        {
            _repo.Setup(r => r.GetAllBrandsAsync(It.IsAny<PageRequest>()))
                .ThrowsAsync(new Exception("X"));

            var result = await _service.GetAllBrandsAsync(new PageRequest(1, 10));

            result.Success.Should().BeFalse();
            result.Error!.Should().Contain("unexpected");
        }

        // ----------------------------------------------------------------------
        // CountAllBrandsAsync
        // ----------------------------------------------------------------------
        [Test]
        public async Task CountAllBrandsAsync_ShouldReturnOk()
        {
            _repo.Setup(r => r.CountAllBrandsAsync())
                .ReturnsAsync(OperationResult<int>.Ok(5));

            var result = await _service.CountAllBrandsAsync();

            result.Success.Should().BeTrue();
            result.Data.Should().Be(5);
        }

        [Test]
        public async Task CountAllBrandsAsync_ShouldReturnFail_OnException()
        {
            _repo.Setup(r => r.CountAllBrandsAsync())
                .ThrowsAsync(new Exception("Boom"));

            var result = await _service.CountAllBrandsAsync();

            result.Success.Should().BeFalse();
        }

        // ----------------------------------------------------------------------
        // GetAllItemBrandsAsync
        // ----------------------------------------------------------------------
        [Test]
        public async Task GetAllItemBrandsAsync_ShouldReturnOk()
        {
            var items = new List<Brand> 
            { 
                new Brand
                {
                    BrandName = "Item",
                    BrandCode = "ItemCODE",
                    BrandDescription = "NICE ITEM",
                    BrandImage = "LOTSOFIMAGE.jpg",
                    Id = Guid.NewGuid(),
                    Products = new List<Product>
                    {
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            ProductName = "Test Product",
                            Sku = "SKU1",
                            Stock = 10,
                            Price = 100,
                            ProductDescription = "Test",
                            Images = new List<Image>
                            {
                                new Image { ImageName = "IMG1", ImagePath = "idle.jpg" }
                            }
                        }
                    }
                }
            };

            var data = new PageResult<Brand>(items, 1, 1, 10);

            _repo.Setup(r => r.GetAllItemBrandsAsync(It.IsAny<PageRequest>(), It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<PageResult<Brand>>.Ok(data));

            var result = await _service.GetAllItemBrandsAsync(new PageRequest(1, 10), Guid.NewGuid());

            result.Success.Should().BeTrue();
            result.Data!.Items.Should().HaveCount(1);
        }


        [Test]
        public async Task GetAllItemBrandsAsync_ShouldFail_WhenRepositoryFail()
        {
            _repo.Setup(r => r.GetAllItemBrandsAsync(It.IsAny<PageRequest>(), It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<PageResult<Brand>>.Fail("Err"));

            var result = await _service.GetAllItemBrandsAsync(new PageRequest(1, 10), Guid.NewGuid());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Err");
        }

        [Test]
        public async Task GetAllItemBrandsAsync_ShouldFail_WhenNoItems()
        {
            var empty = new PageResult<Brand>(new List<Brand>(), 0, 1, 10);

            _repo.Setup(r => r.GetAllItemBrandsAsync(It.IsAny<PageRequest>(), It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<PageResult<Brand>>.Ok(empty));

            var result = await _service.GetAllItemBrandsAsync(new PageRequest(1, 10), Guid.NewGuid());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("No items found");
        }

        [Test]
        public async Task GetAllItemBrandsAsync_ShouldFail_OnException()
        {
            _repo.Setup(r => r.GetAllItemBrandsAsync(It.IsAny<PageRequest>(), It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("X"));

            var result = await _service.GetAllItemBrandsAsync(new PageRequest(1, 10), Guid.NewGuid());

            result.Success.Should().BeFalse();
        }

        // ----------------------------------------------------------------------
        // GetBrandAsync
        // ----------------------------------------------------------------------
        [Test]
        public async Task GetBrandAsync_ShouldReturnOk_WhenFound()
        {
            _repo.Setup(r => r.GetBrandAsync(It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<Brand>.Ok(new Brand
                {
                    BrandCode = "WEK",
                    BrandName = "WeKnow",
                    BrandDescription = "Some description",
                    BrandImage = "JUNK.jpg"
                }));

            var result = await _service.GetBrandAsync(Guid.NewGuid());

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task GetBrandAsync_ShouldReturnFail_WhenNotFound()
        {
            _repo.Setup(r => r.GetBrandAsync(It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<Brand>.Ok(null!));

            var result = await _service.GetBrandAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Brand not found");
        }

        [Test]
        public async Task GetBrandAsync_ShouldReturnFail_OnException()
        {
            _repo.Setup(r => r.GetBrandAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Error"));

            var result = await _service.GetBrandAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
        }

        // ----------------------------------------------------------------------
        // DeleteBrandAsync
        // ----------------------------------------------------------------------
        [Test]
        public async Task DeleteBrandAsync_ShouldReturnOk()
        {
            _repo.Setup(r => r.DeleteBrandAsync(It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<Brand>.Ok(new Brand
                {
                    BrandCode = "OLDCODE",
                    BrandName = "Deleted Brand",
                    BrandDescription = "Description",
                    BrandImage = "DELETED.jpg"
                }));

            var result = await _service.DeleteBrandAsync(Guid.NewGuid());

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task DeleteBrandAsync_ShouldReturnFail_OnException()
        {
            _repo.Setup(r => r.DeleteBrandAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Err"));

            var result = await _service.DeleteBrandAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
        }

        // ----------------------------------------------------------------------
        // UpdateBrandAsync
        // ----------------------------------------------------------------------
        [Test]
        public async Task UpdateBrandAsync_ShouldReturnFail_WhenBrandNotFound()
        {
            _repo.Setup(r => r.GetBrandAsync(It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<Brand>.Fail("not found"));

            var result = await _service.UpdateBrandAsync(Guid.NewGuid().ToString(), new UpdateBrandDto());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("not found");
        }

        [Test]
        public async Task UpdateBrandAsync_ShouldReturnOk_WhenUpdated()
        {
            var brand = new Brand
            {
                BrandCode = "TESTCODE",
                BrandName = "UPD",
                BrandDescription = "LSR ",
                BrandImage = "PATH.jpg"
            };

            _repo.Setup(r => r.GetBrandAsync(It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<Brand>.Ok(brand));

            _repo.Setup(r => r.UpdateBrandAsync(It.IsAny<Brand>()))
                .ReturnsAsync(OperationResult<Brand>.Ok(brand));

            var result = await _service.UpdateBrandAsync(Guid.NewGuid().ToString(), new UpdateBrandDto());

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task UpdateBrandAsync_ShouldFail_OnException()
        {
            _repo.Setup(r => r.GetBrandAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Err"));

            var result = await _service.UpdateBrandAsync(Guid.NewGuid().ToString(), new UpdateBrandDto());

            result.Success.Should().BeFalse();
        }

        // ----------------------------------------------------------------------
        // CreateBrandAsync
        // ----------------------------------------------------------------------
        [Test]
        public async Task CreateBrandAsync_ShouldFail_WhenCodeExists()
        {
            _repo.Setup(r => r.GetBrandByBrandCodeAsync(It.IsAny<string>()))
                .ReturnsAsync(OperationResult<Brand>.Ok(new Brand
                {
                    BrandCode = "JDQ",
                    BrandName = "Lucky Dog",
                    BrandDescription = "Some description",
                    BrandImage = "Some image.jpg"
                }));

            var dto = new CreateBrandDto { BrandCode = "ABC" };

            var result = await _service.CreateBrandAsync(dto);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Brand code already existed");
        }

        [Test]
        public async Task CreateBrandAsync_ShouldCreate_WhenCodeNotExists()
        {
            _repo.Setup(r => r.GetBrandByBrandCodeAsync(It.IsAny<string>()))
                .ReturnsAsync(OperationResult<Brand>.Fail("not found"));

            _repo.Setup(r => r.AddBrandAsync(It.IsAny<Brand>()))
                .ReturnsAsync(OperationResult<Brand>.Ok(new Brand
                {
                    BrandCode = "CODE",
                    BrandName = "TEST",
                    BrandDescription = "DESP",
                    BrandImage = "path/to/image.jpg"
                }));

            var result = await _service.CreateBrandAsync(new CreateBrandDto { BrandCode = "NEW" });

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task CreateBrandAsync_ShouldFail_OnDbException()
        {
            _repo.Setup(r => r.GetBrandByBrandCodeAsync(It.IsAny<string>()))
                .ReturnsAsync(OperationResult<Brand>.Fail("not found"));

            _repo.Setup(r => r.AddBrandAsync(It.IsAny<Brand>()))
                .ThrowsAsync(new Microsoft.EntityFrameworkCore.DbUpdateException("db", new Exception()));

            var result = await _service.CreateBrandAsync(new CreateBrandDto());

            result.Success.Should().BeFalse();
        }
    }
}
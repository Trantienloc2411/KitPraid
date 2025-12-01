using FluentAssertions;
using ProductService.Application.Dtos;
using ProductService.Application.Mapping;
using ProductService.Domain.Entities;

namespace ProductService.Application.Test.Mapping
{
    [TestFixture]
    public class BrandMapperTests
    {
        [Test]
        public void ToBrand_ShouldMapCreateBrandDtoToBrand()
        {
            var dto = new CreateBrandDto
            {
                BrandCode = "CODE123",
                BrandName = "BrandX",
                Description = "DescriptionX",
                BrandImageUrl = "http://image.url",
                IsActive = true
            };

            var brand = BrandMapper.ToBrand(dto);

            brand.Id.Should().NotBeEmpty();
            brand.BrandCode.Should().Be("CODE123");
            brand.BrandName.Should().Be("BrandX");
            brand.BrandDescription.Should().Be("DescriptionX");
            brand.BrandImage.Should().Be("http://image.url");
            brand.IsActive.Should().BeTrue();
            brand.IsDeleted.Should().BeFalse();
        }

        [Test]
        public void ApplyUpdate_ShouldUpdateBrandProperties()
        {
            var brand = new Brand
            {
                BrandName = "OldName",
                BrandDescription = "OldDesc",
                BrandImage = "OldImage",
                IsActive = false,
                DateModified = DateTime.UtcNow.AddDays(-1),
                BrandCode = "CODE"
            };

            var dto = new UpdateBrandDto
            {
                BrandName = "NewName", Description = "NewDesc", BrandImageUrl = "NewImage", IsActive = true
            };

            BrandMapper.ApplyUpdate(brand, dto);

            brand.BrandName.Should().Be("NewName");
            brand.BrandDescription.Should().Be("NewDesc");
            brand.BrandImage.Should().Be("NewImage");
            brand.IsActive.Should().BeTrue();
            brand.DateModified.Should().BeAfter(DateTime.UtcNow.AddSeconds(-5));
        }

        [Test]
        public void ToGetBrandDto_ShouldMapBrandToGetBrandDto()
        {
            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandCode = "CODE",
                BrandName = "BrandX",
                BrandDescription = "Desc",
                BrandImage = "ImageUrl"
            };

            var dto = BrandMapper.ToGetBrandDto(brand);

            dto.Id.Should().Be(brand.Id);
            dto.BrandCode.Should().Be("CODE");
            dto.BrandName.Should().Be("BrandX");
            dto.Description.Should().Be("Desc");
            dto.BrandImageUrl.Should().Be("ImageUrl");
        }

        [Test]
        public void ToGetBrandItemDto_ShouldMapBrandWithProducts()
        {
            // Arrange
            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandName = "BrandX",
                BrandCode = "CODE",
                BrandDescription = "DES",
                BrandImage = "PATHTOIMAHGE",
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "P1",
                        Sku = "SKU1",
                        Stock = 10,
                        Price = 100,
                        CategoryId = "CAT1",
                        ProductDescription = "DES3",
                        Images = new List<Image> { new Image { ImageName = "IMG1", ImagePath = "idle.jpg" } },
                        IsActive = true,
                        IsDeleted = false,
                        UserId = Guid.NewGuid(),
                        Created = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "P2",
                        Sku = "SKU2",
                        Stock = 5,
                        Price = 50,
                        CategoryId = "CAT2",
                        ProductDescription = "DES4",
                        Images = new List<Image> { new Image { ImageName = "IMG2", ImagePath = "idle.jpg" } },
                        IsActive = true,
                        IsDeleted = false,
                        UserId = Guid.NewGuid(),
                        Created = DateTime.UtcNow
                    }
                }
            };

            // Act
            var dto = BrandMapper.ToGetBrandItemDto(brand);

            // Assert
            dto.BrandId.Should().Be(brand.Id);
            dto.BrandName.Should().Be("BrandX");
            dto.Products.Should().HaveCount(2);
            dto.Products.Select(p => p.ProductName).Should().Contain(new[] { "P1", "P2" });
            dto.Products.SelectMany(p => p.Images).Select(i => i.ImageName).Should().Contain(new[] { "IMG1", "IMG2" });
        }
    }

}
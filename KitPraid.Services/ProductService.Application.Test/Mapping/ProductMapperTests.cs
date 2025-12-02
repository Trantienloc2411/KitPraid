using FluentAssertions;
using ProductService.Application.Dtos;
using ProductService.Application.Mapping;
using ProductService.Domain.Entities;

namespace ProductService.Application.Test.Mapping
{
    [TestFixture]
    public class ProductMapperTests
    {
        [Test]
    public void ToCreateProduct_ShouldMapAllFieldsCorrectly()
    {
        // Arrange
        var dto = new CreateProductDto
        {
            ProductName = "Test Product",
            ProductDescription = "This is a description of the test product.",
            Price = 100,
            Sku = "SKU123",
            BrandId = Guid.NewGuid(),
            CategoryId = "CAT1",
            Stock = 10,
            IsActive = true,
            Attributes = new Dictionary<string, object> { { "Color", "Red" } },
            UserId = Guid.NewGuid()
        };

        // Act
        var product = ProductMapper.ToCreateProduct(dto);

        // Assert
        product.Id.Should().NotBeEmpty();
        product.ProductName.Should().Be(dto.ProductName);
        product.ProductDescription.Should().Be(dto.ProductDescription);
        product.Price.Should().Be(dto.Price);
        product.Sku.Should().Be(dto.Sku);
        product.BrandId.Should().Be(dto.BrandId);
        product.CategoryId.Should().Be(dto.CategoryId);
        product.Stock.Should().Be(dto.Stock);
        product.IsActive.Should().BeTrue();
        product.IsDeleted.Should().BeFalse();
        product.UserId.Should().Be(dto.UserId);
        product.Attributes.Should().ContainKey("Color").WhoseValue!.ToString().Should().Be("Red");
        product.Created.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void ApplyUpdate_ShouldUpdateOnlyProvidedFields()
    {
        // Arrange
        var product = new Product
        {
            ProductName = "Old Name",
            ProductDescription = "Old Desc",
            Price = 50,
            Stock = 5,
            CategoryId = "CAT1",
            IsActive = false,
            Attributes = new Dictionary<string, object>
            {
                { "Size", "M" }
            }!,
            Sku = "prod"
        };

        var dto = new UpdateProductDto
        {
            ProductName = "New Name",
            ProductDescription = "New Desc",
            Price = 100,
            Stock = 10,
            CategoryId = "CAT2",
            IsActive = true,
            Attributes = new Dictionary<string, object> { { "Size", "L" } }!
        };

        // Act
        ProductMapper.ApplyUpdate(product, dto);

        // Assert
        product.ProductName.Should().Be("New Name");
        product.ProductDescription.Should().Be("New Desc");
        product.Price.Should().Be(100);
        product.Stock.Should().Be(10);
        product.CategoryId.Should().Be("CAT2");
        product.IsActive.Should().BeTrue();
        product.Attributes.Should().ContainKey("Size").WhoseValue!.ToString().Should().Be("L");
        product.Modified.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void ToGetProductDto_ShouldMapCorrectly()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "Product1",
            Sku = "SKU1",
            Price = 100,
            Stock = 5,
            Attributes = new Dictionary<string, object>
            {
                { "Color", "Blue" }
            }!,
            Brand = new Brand
            {
                BrandName = "BrandX",
                BrandCode = "BX",
                BrandDescription = "BDES",
                BrandImage = "PATHTOIMAGE"
            },
            CategoryId = "CAT1",
            Images = new List<Image>
            {
                new Image
                {
                    ImageName = "IMG1",
                    ImagePath = "path.jpg"
                }
            },
            ProductDescription = "DESP"
        };

        // Act
        var dto = ProductMapper.ToGetProductDto(product);

        // Assert
        dto.Id.Should().Be(product.Id);
        dto.ProductName.Should().Be(product.ProductName);
        dto.Sku.Should().Be(product.Sku);
        dto.Price.Should().Be(product.Price);
        dto.Stock.Should().Be(product.Stock);
        dto.Attributes!["Color"]!.ToString().Should().Be("Blue");
        dto.BrandName.Should().Be("BrandX");
        dto.CategoryId.Should().Be("CAT1");
        dto.Images.Should().HaveCount(1);
        dto.Images![0].ImageName.Should().Be("IMG1");
    }

    [Test]
    public void ToGetProductDetailDto_ShouldMapCorrectly()
    {
        // Arrange
        var brand = new Brand
        {
            Id = Guid.NewGuid(),
            BrandName = "BrandX",
            BrandCode = "CODE",
            BrandDescription = "DES",
            BrandImage = "PATH"
        };

        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "Product1",
            ProductDescription = "Description",
            Sku = "SKU1",
            Price = 100,
            Stock = 5,
            Attributes = new Dictionary<string, object> { { "Color", "Blue" } }!,
            Brand = brand,
            CategoryId = "CAT1",
            Images = new List<Image> { new Image { ImageName = "IMG1", ImagePath = "path.jpg" } }
        };

        // Act
        var dto = ProductMapper.ToGetProductDetailDto(product);

        // Assert
        dto.ProductId.Should().Be(product.Id);
        dto.ProductName.Should().Be(product.ProductName);
        dto.ProductDescription.Should().Be(product.ProductDescription);
        dto.Sku.Should().Be(product.Sku);
        dto.Price.Should().Be(product.Price);
        dto.Stock.Should().Be(product.Stock);
        dto.Attributes!["Color"]!.ToString().Should().Be("Blue");
        dto.BrandDto.Should().NotBeNull();
        dto.BrandDto!.BrandName.Should().Be("BrandX");
        dto.CategoryId.Should().Be("CAT1");
        dto.Images.Should().HaveCount(1);
        dto.Images[0].ImageName.Should().Be("IMG1");
    }

    }
}
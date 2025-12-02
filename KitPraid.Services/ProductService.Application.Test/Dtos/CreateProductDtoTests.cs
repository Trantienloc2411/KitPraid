using FluentAssertions;
using ProductService.Application.Dtos;
using ProductService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class CreateProductDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var dto = new CreateProductDto();

            // Assert default values
            dto.ProductName.Should().BeEmpty();
            dto.ProductDescription.Should().BeEmpty();
            dto.Price.Should().Be(0);
            dto.BrandId.Should().Be(Guid.Empty);
            dto.Sku.Should().BeEmpty();
            dto.Stock.Should().Be(0);
            dto.Attributes.Should().BeNull();
            dto.IsActive.Should().BeFalse();
            dto.IsDeleted.Should().BeFalse();
            dto.Images.Should().BeNull();
            dto.UserId.Should().Be(Guid.Empty);
            dto.CategoryId.Should().BeNull();
            dto.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            dto.Modified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var attributes = new Dictionary<string, object> { { "Color", "Red" } };
            var images = new List<Image> { new Image { ImageName = "image1" , ImagePath = "path/to/image.jpg"} };
            var now = DateTime.UtcNow;

            var dto = new CreateProductDto
            {
                ProductName = "Keyboard",
                ProductDescription = "Mechanical keyboard with RGB lighting",
                Price = 199.99,
                BrandId = Guid.NewGuid(),
                Sku = "KB-001",
                Stock = 50,
                Attributes = attributes,
                IsActive = true,
                IsDeleted = false,
                Images = images,
                UserId = Guid.NewGuid(),
                CategoryId = "CAT123",
                Created = now,
                Modified = now
            };

            // Act & Assert
            dto.ProductName.Should().Be("Keyboard");
            dto.ProductDescription.Should().Be("Mechanical keyboard with RGB lighting");
            dto.Price.Should().Be(199.99);
            dto.BrandId.Should().NotBe(Guid.Empty);
            dto.Sku.Should().Be("KB-001");
            dto.Stock.Should().Be(50);
            dto.Attributes.Should().BeEquivalentTo(attributes);
            dto.IsActive.Should().BeTrue();
            dto.IsDeleted.Should().BeFalse();
            dto.Images.Should().BeEquivalentTo(images);
            dto.UserId.Should().NotBe(Guid.Empty);
            dto.CategoryId.Should().Be("CAT123");
            dto.Created.Should().Be(now);
            dto.Modified.Should().Be(now);
        }

        [Test]
        public void ShouldFailValidation_WhenRequiredFieldsMissing()
        {
            // Arrange
            var dto = new CreateProductDto(); // empty object

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            isValid.Should().BeFalse();
            results.Should().Contain(r => r.ErrorMessage!.Contains("Name is required"));
            results.Should().Contain(r => r.ErrorMessage != null && r.ErrorMessage.Contains("Description is required"));
            results.Should().Contain(r => r.ErrorMessage!.Contains("Price must be between 1 and 99999999999"));
            results.Should().Contain(r => r.ErrorMessage!.Contains("Sku is required"));
            results.Should().Contain(r => r.ErrorMessage!.Contains("Attributes is required"));
        }

        [Test]
        public void ShouldFailValidation_WhenStringTooShortOrLong()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                ProductName = "abc", // too short
                ProductDescription = "short desc", // too short
                Price = 10,
                BrandId = Guid.NewGuid(),
                Sku = "SKU001",
                Attributes = new Dictionary<string, object>()
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            isValid.Should().BeFalse();
            results.Should().Contain(r => r.ErrorMessage!.Contains("At least 5 chars"));
            results.Should().Contain(r => r.ErrorMessage!.Contains("At least 20 chars"));
        }

        [Test]
        public void ShouldFailValidation_WhenPriceOrStockOutOfRange()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                ProductName = "Valid Name",
                ProductDescription = "This description is long enough to pass validation",
                Price = 0, // invalid
                BrandId = Guid.NewGuid(),
                Sku = "SKU001",
                Stock = 1000, // invalid
                Attributes = new Dictionary<string, object>()
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            isValid.Should().BeFalse();
            results.Should().Contain(r => r.ErrorMessage!.Contains("Price must be between 1"));
            results.Should().Contain(r => r.ErrorMessage!.Contains("Range must be between 0 and 999"));
        }
    }
}
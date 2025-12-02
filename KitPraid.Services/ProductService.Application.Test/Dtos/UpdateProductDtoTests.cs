using FluentAssertions;
using ProductService.Application.Dtos;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class UpdateProductDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeDefaults()
        {
            var dto = new UpdateProductDto();

            dto.ProductName.Should().BeNull();
            dto.ProductDescription.Should().BeNull();
            dto.BrandId.Should().BeNull();
            dto.Stock.Should().Be(0);
            dto.Attributes.Should().BeNull();
            dto.Price.Should().Be(0);
            dto.CategoryId.Should().BeNull();
            dto.IsActive.Should().BeNull();
            dto.Modified.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            var attributes = new Dictionary<string, object?> { { "Color", "Black" } };
            var modifiedDate = DateTime.UtcNow.AddMinutes(-5);

            var dto = new UpdateProductDto
            {
                ProductName = "Updated Mouse",
                ProductDescription = "Updated description for mouse",
                BrandId = Guid.NewGuid(),
                Stock = 25,
                Attributes = attributes,
                Price = 99.99,
                CategoryId = "CAT03",
                IsActive = true,
                Modified = modifiedDate
            };

            dto.ProductName.Should().Be("Updated Mouse");
            dto.ProductDescription.Should().Be("Updated description for mouse");
            dto.BrandId.Should().NotBeNull();
            dto.Stock.Should().Be(25);
            dto.Attributes.Should().BeEquivalentTo(attributes);
            dto.Price.Should().Be(99.99);
            dto.CategoryId.Should().Be("CAT03");
            dto.IsActive.Should().BeTrue();
            dto.Modified.Should().Be(modifiedDate);
        }
    }
}
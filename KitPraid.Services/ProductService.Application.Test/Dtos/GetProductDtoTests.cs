using FluentAssertions;
using ProductService.Application.Dtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class GetProductDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeProperties()
        {
            var dto = new GetProductDto();

            dto.Id.Should().Be(Guid.Empty);
            dto.ProductName.Should().BeEmpty();
            dto.Sku.Should().BeEmpty();
            dto.Stock.Should().Be(0);
            dto.Attributes.Should().BeEmpty();
            dto.Price.Should().Be(0);
            dto.CategoryId.Should().BeNull();
            dto.Images.Should().BeNull();
            dto.BrandName.Should().BeEmpty();
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            var attributes = new Dictionary<string, object?> { { "Color", "Blue" } };
            var images = new List<Image> { new Image { ImageName = "img1" , ImagePath = "path/to/img.jpg" } };

            var dto = new GetProductDto
            {
                Id = Guid.NewGuid(),
                ProductName = "Mouse",
                Sku = "M001",
                Stock = 5,
                Attributes = attributes,
                Price = 50,
                CategoryId = "CAT02",
                Images = images,
                BrandName = "BrandTest"
            };

            dto.Id.Should().NotBe(Guid.Empty);
            dto.ProductName.Should().Be("Mouse");
            dto.Sku.Should().Be("M001");
            dto.Stock.Should().Be(5);
            dto.Attributes["Color"]!.ToString().Should().Be("Blue");
            dto.Price.Should().Be(50);
            dto.CategoryId.Should().Be("CAT02");
            dto.Images.Should().BeEquivalentTo(images);
            dto.BrandName.Should().Be("BrandTest");
        }
    }
}
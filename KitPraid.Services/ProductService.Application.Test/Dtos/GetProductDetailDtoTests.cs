using FluentAssertions;
using ProductService.Application.Dtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class GetProductDetailDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeProperties()
        {
            var dto = new GetProductDetailDto();

            dto.ProductId.Should().Be(Guid.Empty);
            dto.ProductName.Should().BeEmpty();
            dto.ProductDescription.Should().BeEmpty();
            dto.Sku.Should().BeEmpty();
            dto.CategoryId.Should().BeNull();
            dto.Price.Should().Be(0);
            dto.Stock.Should().Be(0);
            dto.Attributes.Should().BeNullOrEmpty();
            dto.Images.Should().BeEmpty();
            dto.BrandDto.Should().BeNull();
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            var attributes = new Dictionary<string, object?> { { "Color", "Red" } };
            var images = new List<Image> { new Image { ImageName = "img1", ImagePath = "path/to/img.jpg"} };
            var brandDto = new GetBrandDto { BrandName = "MyBrand" };

            var dto = new GetProductDetailDto
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Keyboard",
                ProductDescription = "Mechanical Keyboard",
                Sku = "KB001",
                CategoryId = "CAT01",
                Price = 150,
                Stock = 10,
                Attributes = attributes,
                Images = images,
                BrandDto = brandDto
            };

            dto.ProductName.Should().Be("Keyboard");
            dto.ProductDescription.Should().Be("Mechanical Keyboard");
            dto.Sku.Should().Be("KB001");
            dto.CategoryId.Should().Be("CAT01");
            dto.Price.Should().Be(150);
            dto.Stock.Should().Be(10);
            dto.Attributes.Should().BeEquivalentTo(attributes);
            dto.Images.Should().BeEquivalentTo(images);
            dto.BrandDto.Should().Be(brandDto);
        }
    }
}
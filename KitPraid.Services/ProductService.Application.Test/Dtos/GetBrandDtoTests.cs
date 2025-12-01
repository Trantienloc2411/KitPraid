using FluentAssertions;
using ProductService.Application.Dtos;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class GetBrandDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var dto = new GetBrandDto();

            // Assert
            dto.Id.Should().Be(Guid.Empty);
            dto.BrandCode.Should().BeEmpty();
            dto.BrandName.Should().BeEmpty();
            dto.Description.Should().BeNull();
            dto.BrandImageUrl.Should().BeNull();
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new GetBrandDto
            {
                Id = id,
                BrandCode = "BR01",
                BrandName = "MyBrand",
                Description = "Brand description",
                BrandImageUrl = "https://example.com/image.png"
            };

            // Act & Assert
            dto.Id.Should().Be(id);
            dto.BrandCode.Should().Be("BR01");
            dto.BrandName.Should().Be("MyBrand");
            dto.Description.Should().Be("Brand description");
            dto.BrandImageUrl.Should().Be("https://example.com/image.png");
        }
    }
}
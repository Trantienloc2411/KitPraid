using FluentAssertions;
using ProductService.Application.Dtos;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class CreateBrandDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var dto = new CreateBrandDto();

            // Assert
            dto.BrandCode.Should().BeEmpty(); // default value
            dto.BrandName.Should().BeEmpty(); // default value
            dto.Description.Should().BeNull(); // nullable
            dto.BrandImageUrl.Should().BeNull(); // nullable
            dto.IsActive.Should().BeFalse(); // default bool
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var dto = new CreateBrandDto
            {
                BrandCode = "ABCD1234",
                BrandName = "MyBrand",
                Description = "Brand Description",
                BrandImageUrl = "https://example.com/image.png",
                IsActive = true
            };

            // Act & Assert
            dto.BrandCode.Should().Be("ABCD1234");
            dto.BrandName.Should().Be("MyBrand");
            dto.Description.Should().Be("Brand Description");
            dto.BrandImageUrl.Should().Be("https://example.com/image.png");
            dto.IsActive.Should().BeTrue();
        }

        [Test]
        public void Should_BrandCode_DefaultEmpty_WhenNotProvided()
        {
            var dto = new CreateBrandDto
            {
                BrandName = "TestBrand"
                // BrandCode not set
            };

            dto.BrandCode.Should().BeEmpty();
            dto.BrandName.Should().Be("TestBrand");
        }
    }
}
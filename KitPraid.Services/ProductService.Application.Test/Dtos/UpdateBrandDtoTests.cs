using FluentAssertions;
using ProductService.Application.Dtos;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class UpdateBrandDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeDefaults()
        {
            var dto = new UpdateBrandDto();

            dto.BrandName.Should().BeNull();
            dto.Description.Should().BeNull();
            dto.BrandImageUrl.Should().BeNull();
            dto.IsActive.Should().BeFalse();
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            var dto = new UpdateBrandDto
            {
                BrandName = "NewBrand",
                Description = "Updated desc",
                BrandImageUrl = "https://example.com/image.png",
                IsActive = true
            };

            dto.BrandName.Should().Be("NewBrand");
            dto.Description.Should().Be("Updated desc");
            dto.BrandImageUrl.Should().Be("https://example.com/image.png");
            dto.IsActive.Should().BeTrue();
        }
    }
}
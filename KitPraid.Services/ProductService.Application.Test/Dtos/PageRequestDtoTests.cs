using FluentAssertions;
using ProductService.Application.Dtos;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class PageRequestDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeDefaults()
        {
            var dto = new PageRequestDto();

            dto.Page.Should().Be(1);
            dto.Size.Should().Be(10);
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            var dto = new PageRequestDto { Page = 5, Size = 50 };

            dto.Page.Should().Be(5);
            dto.Size.Should().Be(50);
        }
    }
}
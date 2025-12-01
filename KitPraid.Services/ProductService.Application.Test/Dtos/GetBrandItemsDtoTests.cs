using FluentAssertions;
using ProductService.Application.Dtos;

namespace ProductService.Application.Test.Dtos
{
    [TestFixture]
    public class GetBrandItemsDtoTests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeProperties()
        {
            var dto = new GetBrandItemsDto();

            dto.BrandId.Should().Be(Guid.Empty);
            dto.BrandName.Should().BeEmpty();
            dto.Products.Should().BeEmpty();
        }

        [Test]
        public void Should_SetAndGetPropertiesCorrectly()
        {
            var products = new List<GetProductDto> { new GetProductDto { Id = Guid.NewGuid(), ProductName = "Prod1" } };

            var dto = new GetBrandItemsDto { BrandId = Guid.NewGuid(), BrandName = "BrandTest", Products = products };

            dto.BrandId.Should().NotBe(Guid.Empty);
            dto.BrandName.Should().Be("BrandTest");
            dto.Products.Should().BeEquivalentTo(products);
        }
    }
}
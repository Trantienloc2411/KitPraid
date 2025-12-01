using FluentAssertions;
using ProductService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Test.Entities
{
    public class BrandTests
    {
        private Brand CreateValidBrand()
        {
            return new Brand
            {
                BrandCode = "KB",
                BrandName = "Keychron",
                BrandDescription = "Keyboard brand",
                BrandImage = "https://example.com/logo.png",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }

        [Test]
        public void Brand_ShouldCreateSuccessfully_WithValidData()
        {
            var brand = CreateValidBrand();

            var results = ValidateModel(brand);

            results.Should().BeEmpty();
        }

        [Test]
        public void Brand_ShouldFailValidation_WhenBrandCodeIsTooLong()
        {
            var brand = CreateValidBrand();
            brand.BrandCode = "THIS_IS_TOO_LONG_FOR_BRAND_CODE";

            var results = ValidateModel(brand);

            results.Should().ContainSingle()
                .Which.ErrorMessage.Should().Contain("maximum length");
        }

        [Test]
        public void Brand_ShouldFailValidation_WhenImageIsNotUrl()
        {
            var brand = CreateValidBrand();
            brand.BrandImage = "not-a-url";

            var results = ValidateModel(brand);

            results.Should().ContainSingle()
                .Which.ErrorMessage.Should().Contain("not a valid fully-qualified http");
        }

        [Test]
        public void Brand_ShouldAllowEmptyProductsCollection()
        {
            var brand = CreateValidBrand();

            brand.Products.Should().BeNull(); // default
        }

        [Test]
        public void Brand_ShouldAllowSettingProductsCollection()
        {
            var brand = CreateValidBrand();

            brand.Products = new List<Product>();

            brand.Products.Should().NotBeNull();
            brand.Products.Should().BeEmpty();
        }

        [Test]
        public void Brand_ShouldHaveDefaultBooleanValues_WhenNotSet()
        {
            var brand = new Brand
            {
                BrandCode = "AB",
                BrandName = "TestBrand",
                BrandDescription = "TestDesc",
                BrandImage = "https://example.com/logo.png",
            };

            brand.IsActive.Should().BeFalse();
            brand.IsDeleted.Should().BeFalse();
        }
    }
}
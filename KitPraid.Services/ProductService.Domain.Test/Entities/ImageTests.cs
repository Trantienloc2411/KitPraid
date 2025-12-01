using FluentAssertions;
using ProductService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Test.Entities
{
    public class ImageTests
    {
        private Image CreateValidImage()
        {
            return new Image
            {
                ImageName = "keyboard.png",
                ImagePath = "/uploads/keyboards/keyboard.png",
                ProductId = Guid.NewGuid(),
                IsActive = true,
                IsDeleted = false
            };
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Test]
        public void Image_ShouldCreateSuccessfully_WithValidData()
        {
            var image = CreateValidImage();

            var results = ValidateModel(image);

            results.Should().BeEmpty();
        }

        [Test]
        public void Image_ShouldFailValidation_WhenImageNameMissing()
        {
            var image = CreateValidImage();
            image.ImageName = null!;

            var results = ValidateModel(image);

            results.Should().ContainSingle()
                .Which.ErrorMessage.Should().Contain("required");
        }

        [Test]
        public void Image_ShouldFailValidation_WhenImagePathMissing()
        {
            var image = CreateValidImage();
            image.ImagePath = null!;

            var results = ValidateModel(image);

            results.Should().ContainSingle()
                .Which.ErrorMessage.Should().Contain("required");
        }

        [Test]
        public void Image_ShouldHaveDefaultBooleanValues()
        {
            var image = new Image { ImageName = "test.png", ImagePath = "/img/test.png", };

            image.IsActive.Should().BeFalse();
            image.IsDeleted.Should().BeFalse();
        }

        [Test]
        public void Image_ProductNavigation_ShouldBeNullByDefault()
        {
            var image = CreateValidImage();

            image.Product.Should().BeNull();
        }

        [Test]
        public void Image_CanAssignProductNavigation()
        {
            var image = CreateValidImage();

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Test Keyboard",
                ProductDescription = "Mechanical",
                Sku = "KB1234",
                Stock = 10
            };

            image.Product = product;

            image.Product.Should().NotBeNull();
            image.Product.ProductName.Should().Be("Test Keyboard");
        }
    }
}
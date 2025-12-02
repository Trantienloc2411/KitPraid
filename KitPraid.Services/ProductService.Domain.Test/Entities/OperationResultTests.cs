using FluentAssertions;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Test.Entities
{
    public class OperationResultTests
    {
        [Test]
        public void Ok_ShouldReturnSuccessResult_WithData()
        {
            // Arrange
            var data = "Hello World";

            // Act
            var result = OperationResult<string>.Ok(data);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().Be(data);
            result.Error.Should().BeNull();
        }

        [Test]
        public void Fail_ShouldReturnFailedResult_WithError()
        {
            // Arrange
            var errorMessage = "Something went wrong";

            // Act
            var result = OperationResult<string>.Fail(errorMessage);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be(errorMessage);
            result.Data.Should().BeNull();
        }

        [Test]
        public void Ok_ShouldWorkWithComplexType()
        {
            var product = new Product
            {
                ProductName = "Keyboard",
                ProductDescription = "Mechanical",
                Sku = "KB123",
            };

            var result = OperationResult<Product>.Ok(product);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.ProductName.Should().Be("Keyboard");
        }

        [Test]
        public void Fail_ShouldWorkWithDifferentGenericType()
        {
            var result = OperationResult<int>.Fail("Invalid id");

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Invalid id");
            result.Data.Should().Be(0);
        }
    }
}
using FluentAssertions;
using ProductService.Domain.Entities;
using System.Text.Json;
// change to your namespace

namespace ProductService.Domain.Test.Entities
{
    public class ProductTests
    {
        [Test]
        public void Attributes_ShouldReturnEmptyDictionary_WhenJsonIsNull()
        {
            Product product = new Product
            {
                ProductName = "Keyboard",
                ProductDescription = "Mechanical",
                Sku = "KB001"
            };

            var attributes = product.Attributes;

            attributes.Should().NotBeNull();
            attributes.Should().BeEmpty();
        }

        [Test]
        public void Attributes_ShouldSerializeDictionary_ToJson()
        {
            var product = new Product
            {
                ProductName = "Keyboard",
                ProductDescription = "Mechanical",
                Sku = "KB001"
            };

            var dict = new Dictionary<string, object?>
            {
                { "Color", "Black" },
                { "Switch", "Cherry Red" },
                { "Weight", 500 }
            };

            // Act
            product.Attributes = dict;

            // Assert — check JSON exists
            product.AttributesJson.Should().NotBeNullOrEmpty();

            // And JSON should contain keys
            product.AttributesJson.Should().Contain("Color");
            product.AttributesJson.Should().Contain("Switch");
            product.AttributesJson.Should().Contain("500"); // weight value
        }

        [Test]
        public void Attributes_ShouldDeserializeJson_ToDictionary()
        {
            var product = new Product
            {
                ProductName = "Keyboard",
                ProductDescription = "Mechanical",
                Sku = "KB001",
                AttributesJson = "{\"Color\":\"White\",\"Keycap\":\"PBT\"}"
            };

            var attributes = product.Attributes;

            attributes.Should().ContainKey("Color");
            attributes["Color"].ToString().Should().Be("White");

            attributes.Should().ContainKey("Keycap");
            attributes["Keycap"].ToString().Should().Be("PBT");
        }

        [Test]
        public void DefaultValues_ShouldBeCorrect()
        {
            var product = new Product
            {
                ProductName = "Keyboard",
                ProductDescription = "Mechanical",
                Sku = "KB001"
            };

            product.Price.Should().Be(0.0);
            product.CategoryId.Should().Be("");  // default set in class
        }

        [Test]
        public void Modified_ShouldBeNull_ByDefault()
        {
            var product = new Product
            {
                ProductName = "Keyboard",
                ProductDescription = "Mechanical",
                Sku = "KB001"
            };

            product.Modified.Should().BeNull();
        }
    }
}

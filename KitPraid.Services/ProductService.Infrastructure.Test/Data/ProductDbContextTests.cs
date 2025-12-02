using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;
using System.Net.Mime;

namespace ProductService.Infrastructure.Test.Data
{
    [TestFixture]
    public class ProductDbContextTests
    {
        private DbContextOptions<ProductDbContext> CreateOptions(string dbName) =>
            new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

        [Test]
        public void CanAddAndRetrieveBrand()
        {
            var options = CreateOptions("BrandDb");
            using var context = new ProductDbContext(options);

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandCode = "ADD",
                BrandName = "Addison-Wesley",
                BrandDescription = "Book brand",
                BrandImage = "https://example.com/logo.png",
                IsActive = true,
                IsDeleted = false,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            context.Brands.Add(brand);
            context.SaveChanges();

            var savedBrand = context.Brands.FirstOrDefault(b => b.Id == brand.Id);
            savedBrand.Should().NotBeNull();
            savedBrand!.BrandName.Should().Be("Addison-Wesley");
        }

        [Test]
        public void Product_ShouldLinkToBrand_AndImages()
        {
            var options = CreateOptions("ProductDb");
            using var context = new ProductDbContext(options);

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandCode = "KBR",
                BrandName = "KeyBrand",
                BrandDescription = "Keyboard",
                BrandImage = "https://example.com/logo.png",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
            context.Brands.Add(brand);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Keyboard 1",
                ProductDescription = "Mechanical Keyboard",
                BrandId = brand.Id,
                Sku = "KB001",
                Stock = 10,
                UserId = Guid.NewGuid()
            };

            product.Images = new[]
            {
                new Image
                {
                    Id = Guid.NewGuid(), ImageName = "img1.png", ImagePath = "/img/img1.png"
                },
                new Image
                {
                    Id = Guid.NewGuid(), ImageName = "img2.png", ImagePath = "/img/img2.png"
                }
            };

            context.Products.Add(product);
            context.SaveChanges();

            var savedProduct = context.Products
                .Include(p => p.Brand)
                .Include(p => p.Images)
                .FirstOrDefault(p => p.Id == product.Id);

            savedProduct.Should().NotBeNull();
            savedProduct!.Brand.Should().NotBeNull();
            savedProduct.Brand!.BrandName.Should().Be("KeyBrand");
            savedProduct.Images.Should().HaveCount(2);
        }

        [Test]
        public void Attributes_ShouldBeIgnoredInDatabase()
        {
            var options = CreateOptions("AttrDb");
            using var context = new ProductDbContext(options);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Test Product",
                ProductDescription = "Desc",
                BrandId = Guid.NewGuid(),
                Sku = "SKU01",
                Stock = 5,
                Attributes = new System.Collections.Generic.Dictionary<string, object?> { ["Color"] = "Red" },
                UserId = Guid.NewGuid()
            };

            context.Products.Add(product);
            context.SaveChanges();

            // Direct EF query: AttributesJson exists, Attributes property ignored
            var savedProduct = context.Products.First();
            savedProduct.Attributes.Should().NotBeNull();
            // EF doesn't track Attributes directly, only AttributesJson
            savedProduct.AttributesJson.Should().NotBeNull();
        }
    }
}
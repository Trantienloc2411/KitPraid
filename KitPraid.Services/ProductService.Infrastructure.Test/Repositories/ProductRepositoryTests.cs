using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repository;

namespace ProductService.Infrastructure.Test.Repositories
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private DbContextOptions<ProductDbContext> CreateOptions(string dbName) =>
            new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

        private ProductRepository CreateRepository(DbContextOptions<ProductDbContext> options)
        {
            var context = new ProductDbContext(options);
            return new ProductRepository(context);
        }

        private Brand SeedBrand(ProductDbContext context, string code = "ABC")
        {
            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandCode = code,
                BrandName = $"Brand {code}",
                BrandDescription = "Desc",
                BrandImage = "https://example.com/logo.png",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            context.Brands.Add(brand);
            context.SaveChanges();
            return brand;
        }

        private Product SeedProduct(ProductDbContext context, Brand? brand = null, string sku = "SKU1",
            bool isDeleted = false, bool isActive = true)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Test Product",
                ProductDescription = "Desc",
                BrandId = (brand ?? SeedBrand(context)).Id,
                Sku = sku,
                Stock = 10,
                IsDeleted = isDeleted,
                IsActive = isActive,
                UserId = Guid.NewGuid(),
                Created = DateTime.UtcNow
            };
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        #region GetAllAsync

        [Test]
        public async Task GetAllAsync_ShouldReturnPagedProducts()
        {
            var options = CreateOptions("GetAllProducts");
            var context = new ProductDbContext(options);
            var repo = new ProductRepository(context);

            for (int i = 0; i < 5; i++)
                SeedProduct(context);

            var pageRequest = new PageRequest(1, 2);
            var result = await repo.GetAllAsync(pageRequest);

            result.Success.Should().BeTrue();
            result.Data!.Items.Should().HaveCount(2);
            result.Data.TotalCount.Should().Be(5);
            result.Data.Page.Should().Be(1);
            result.Data.Size.Should().Be(2);
        }

        #endregion

        #region GetByIdAsync

        [Test]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
        {
            var options = CreateOptions("GetById");
            var context = new ProductDbContext(options);
            var repo = new ProductRepository(context);
            var product = SeedProduct(context);

            var result = await repo.GetByIdAsync(product.Id.ToString());

            result.Success.Should().BeTrue();
            result.Data!.Id.Should().Be(product.Id);
        }

        [Test]
        public async Task GetByIdAsync_ShouldFail_WhenInvalidGuid()
        {
            var options = CreateOptions("GetByIdInvalid");
            var repo = CreateRepository(options);

            var result = await repo.GetByIdAsync("not-guid");

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Invalid product id format.");
        }

        [Test]
        public async Task GetByIdAsync_ShouldFail_WhenNotExists()
        {
            var options = CreateOptions("GetByIdNotExist");
            var repo = CreateRepository(options);

            var result = await repo.GetByIdAsync(Guid.NewGuid().ToString());

            result.Success.Should().BeFalse();
            result.Error.Should().Contain("not found");
        }

        #endregion

        #region SaveAsync

        [Test]
        public async Task SaveAsync_ShouldSaveProduct()
        {
            var options = CreateOptions("SaveProduct");
            var repo = CreateRepository(options);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "New Product",
                ProductDescription = "Desc",
                BrandId = Guid.NewGuid(),
                Sku = "SKU_NEW",
                Stock = 5,
                IsActive = true,
                IsDeleted = false,
                UserId = Guid.NewGuid(),
                Created = DateTime.UtcNow
            };

            var result = await repo.SaveAsync(product);

            result.Success.Should().BeTrue();
            result.Data!.ProductName.Should().Be("New Product");
        }

        #endregion

        #region DeleteAsync

        [Test]
        public async Task DeleteAsync_ShouldMarkProductDeleted_WhenExists()
        {
            var options = CreateOptions("DeleteProduct");
            var context = new ProductDbContext(options);
            var repo = new ProductRepository(context);
            var product = SeedProduct(context);

            var result = await repo.DeleteAsync(product.Id.ToString());

            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();

            var deleted = await context.Products.FindAsync(product.Id);
            deleted!.IsDeleted.Should().BeTrue();
        }

        [Test]
        public async Task DeleteAsync_ShouldFail_WhenInvalidGuid()
        {
            var options = CreateOptions("DeleteInvalidGuid");
            var repo = CreateRepository(options);

            var result = await repo.DeleteAsync("not-guid");

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Invalid product id format.");
        }

        [Test]
        public async Task DeleteAsync_ShouldFail_WhenNotExists()
        {
            var options = CreateOptions("DeleteNotExist");
            var repo = CreateRepository(options);

            var result = await repo.DeleteAsync(Guid.NewGuid().ToString());

            result.Success.Should().BeFalse();
            result.Error.Should().Contain("not found");
        }

        #endregion

        #region GetAllProductsWithKeywordAsync

        [Test]
        public async Task GetAllProductsWithKeywordAsync_ShouldReturnMatchingProducts()
        {
            var options = CreateOptions("GetWithKeyword");
            var context = new ProductDbContext(options);
            var repo = new ProductRepository(context);

            var product1 = SeedProduct(context, sku: "SKU1");
            var product2 = SeedProduct(context, sku: "SKU2", isActive: true, isDeleted: false);

            var pageRequest = new PageRequest(1, 10);
            var result = await repo.GetAllProductsWithKeywordAsync(pageRequest, "SKU1");

            result.Success.Should().BeTrue();
            result.Data!.Items.Should().ContainSingle(p => p.Id == product1.Id);
        }

        #endregion

        #region AddProductAsync

        [Test]
        public async Task AddProductAsync_ShouldAddProduct()
        {
            var options = CreateOptions("AddProduct");
            var context = new ProductDbContext(options);
            var repo = new ProductRepository(context);

            // Seed a Brand
            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandCode = "BR01",
                BrandName = "Brand 1",
                BrandDescription = "Desc",
                BrandImage = "https://example.com/logo.png",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            context.Brands.Add(brand);
            await context.SaveChangesAsync();

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Added Product",
                ProductDescription = "Desc",
                BrandId = brand.Id, 
                Sku = "SKU_ADD",
                Stock = 5,
                IsActive = true,
                IsDeleted = false,
                UserId = Guid.NewGuid(),
                Created = DateTime.UtcNow
            };

            var result = await repo.AddProductAsync(product);

            result.Success.Should().BeTrue();
            result.Data!.Sku.Should().Be("SKU_ADD");
            result.Data.BrandId.Should().Be(brand.Id); // confirm FK
        }


        #endregion

        #region UpdateAsync

        [Test]
        public async Task UpdateAsync_ShouldUpdateProduct()
        {
            var options = CreateOptions("UpdateProduct");
            var context = new ProductDbContext(options);
            var repo = new ProductRepository(context);
            var product = SeedProduct(context);

            product.ProductName = "Updated Name";
            var result = await repo.UpdateAsync(product);

            result.Success.Should().BeTrue();
            result.Data!.ProductName.Should().Be("Updated Name");

            var updated = await context.Products.FindAsync(product.Id);
            updated!.ProductName.Should().Be("Updated Name");
        }

        #endregion

        #region GetProductBySkuAsync

        [Test]
        public async Task GetProductBySkuAsync_ShouldReturnProduct_WhenExists()
        {
            var options = CreateOptions("GetBySku");
            var context = new ProductDbContext(options);
            var repo = new ProductRepository(context);
            var product = SeedProduct(context, sku: "SKU_TEST");

            var result = await repo.GetProductBySkuAsync("SKU_TEST");

            result.Success.Should().BeTrue();
            result.Data!.Sku.Should().Be("SKU_TEST");
        }

        [Test]
        public async Task GetProductBySkuAsync_ShouldFail_WhenNotExists()
        {
            var options = CreateOptions("GetBySkuNotExist");
            var context = new ProductDbContext(options);
            var repo = new ProductRepository(context);

            var result = await repo.GetProductBySkuAsync("NOT_EXIST");

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Product not found.");
        }

        #endregion
    }
}
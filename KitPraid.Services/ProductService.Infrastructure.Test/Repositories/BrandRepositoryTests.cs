using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.ValueObjects;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repository;

namespace ProductService.Infrastructure.Test.Repositories
{
    [TestFixture]
    public class BrandRepositoryTests
    {
        private DbContextOptions<ProductDbContext> CreateOptions(string dbName) =>
            new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

        private BrandRepository CreateRepository(DbContextOptions<ProductDbContext> options)
        {
            var context = new ProductDbContext(options);
            return new BrandRepository(context);
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

        #region GetBrandAsync
        [Test]
        public async Task GetBrandAsync_ShouldReturnBrand_WhenExists()
        {
            var options = CreateOptions("GetBrand");
            var context = new ProductDbContext(options);
            var repo = CreateRepository(options);
            var brand = SeedBrand(context);

            var result = await repo.GetBrandAsync(brand.Id);

            result.Success.Should().BeTrue();
            result.Data!.Id.Should().Be(brand.Id);
        }

        [Test]
        public async Task GetBrandAsync_ShouldFail_WhenNotExists()
        {
            var options = CreateOptions("GetBrandNotExist");
            var repo = CreateRepository(options);

            var result = await repo.GetBrandAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Brand not found.");
        }
        #endregion

        #region AddBrandAsync & SaveBrandAsync
        [Test]
        public async Task AddBrandAsync_ShouldAddBrand()
        {
            var options = CreateOptions("AddBrand");
            var repo = CreateRepository(options);

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandCode = "XYZ",
                BrandName = "New Brand",
                BrandDescription = "Desc",
                BrandImage = "https://example.com/logo.png",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };

            var result = await repo.AddBrandAsync(brand);

            result.Success.Should().BeTrue();
            result.Data!.BrandName.Should().Be("New Brand");
        }

        [Test]
        public async Task SaveBrandAsync_ShouldSaveBrand()
        {
            var options = CreateOptions("SaveBrand");
            var repo = CreateRepository(options);

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandCode = "SAVE",
                BrandName = "Saved Brand",
                BrandDescription = "Desc",
                BrandImage = "https://example.com/logo.png",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            var result = await repo.SaveBrandAsync(brand);

            result.Success.Should().BeTrue();
            result.Data!.BrandCode.Should().Be("SAVE");
        }
        #endregion

        #region UpdateBrandAsync
        [Test]
        public async Task UpdateBrandAsync_ShouldUpdateExistingBrand()
        {
            var options = CreateOptions("UpdateBrand");
            var context = new ProductDbContext(options);
            var repo = new BrandRepository(context);

            var brand = SeedBrand(context, "OLD");
            brand.BrandName = "UpdatedName";
            brand.IsDeleted = true;

            var result = await repo.UpdateBrandAsync(brand);

            result.Success.Should().BeTrue();
            result.Data!.BrandName.Should().Be("UpdatedName");
            result.Data.IsDeleted.Should().BeTrue();
        }
        #endregion

        #region DeleteBrandAsync
        [Test]
        public async Task DeleteBrandAsync_ShouldMarkBrandAndProductsDeleted()
        {
            var options = CreateOptions("DeleteBrand");
            var context = new ProductDbContext(options);
            var repo = new BrandRepository(context);

            var brand = SeedBrand(context);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Product1",
                ProductDescription = "Desc",
                BrandId = brand.Id,
                Sku = "SKU1",
                Stock = 5,
                IsDeleted = false,
                UserId = Guid.NewGuid()
            };
            context.Products.Add(product);
            context.SaveChanges();

            var result = await repo.DeleteBrandAsync(brand.Id);

            result.Success.Should().BeTrue();
            result.Data!.IsDeleted.Should().BeTrue();
            context.Products.First().IsDeleted.Should().BeTrue();
        }
        #endregion

        #region CountAllBrandsAsync
        [Test]
        public async Task CountAllBrandsAsync_ShouldReturnCorrectCount()
        {
            var options = CreateOptions("CountBrands");
            var context = new ProductDbContext(options);
            var repo = new BrandRepository(context);

            SeedBrand(context, "A");
            SeedBrand(context, "B");

            var result = await repo.CountAllBrandsAsync();

            result.Success.Should().BeTrue();
            result.Data.Should().Be(2);
        }
        #endregion

        #region GetAllBrandsAsync
        [Test]
        public async Task GetAllBrandsAsync_ShouldReturnPagedResult()
        {
            var options = CreateOptions("PagedBrands");
            var context = new ProductDbContext(options);
            var repo = new BrandRepository(context);

            for (int i = 1; i <= 5; i++)
                SeedBrand(context, $"Code{i}");

            var pageRequest = new PageRequest(2, 2);
            var result = await repo.GetAllBrandsAsync(pageRequest);

            result.Success.Should().BeTrue();
            result.Data!.Items.Should().HaveCount(2);
            result.Data.Page.Should().Be(2);
            result.Data.Size.Should().Be(2);
            result.Data.TotalCount.Should().Be(5);
            result.Data.HasNextPage.Should().BeTrue();
        }
        #endregion

        #region GetAllItemBrandsAsync
        [Test]
        public async Task GetAllItemBrandsAsync_ShouldReturnBrandWithNonDeletedProducts()
        {
            var options = CreateOptions("ItemBrands");
            var context = new ProductDbContext(options);
            var repo = new BrandRepository(context);

            var brand = SeedBrand(context, "ITEM");
            context.Products.Add(new Product { Id = Guid.NewGuid(), ProductName = "P1", ProductDescription = "Desc", BrandId = brand.Id, Sku = "SKU1", Stock = 5, IsDeleted = false, UserId = Guid.NewGuid() });
            context.Products.Add(new Product { Id = Guid.NewGuid(), ProductName = "P2", ProductDescription = "Desc", BrandId = brand.Id, Sku = "SKU2", Stock = 5, IsDeleted = true, UserId = Guid.NewGuid() });
            context.SaveChanges();

            var pageRequest = new PageRequest(1, 10);
            var result = await repo.GetAllItemBrandsAsync(pageRequest, brand.Id);

            result.Success.Should().BeTrue();
            result.Data!.Items.Should().HaveCount(1);
            result.Data.Items.First().Products.Should().ContainSingle(p => p.IsDeleted == false);
        }
        #endregion

        #region GetBrandByBrandCodeAsync
        [Test]
        public async Task GetBrandByBrandCodeAsync_ShouldReturnBrand_WhenExists()
        {
            var options = CreateOptions("BrandCode");
            var context = new ProductDbContext(options);
            var repo = new BrandRepository(context);
            var brand = SeedBrand(context, "CODE123");

            var result = await repo.GetBrandByBrandCodeAsync("CODE123");

            result.Success.Should().BeTrue();
            result.Data!.BrandCode.Should().Be("CODE123");
        }

        [Test]
        public async Task GetBrandByBrandCodeAsync_ShouldFail_WhenNotExists()
        {
            var options = CreateOptions("BrandCodeNotExist");
            var repo = CreateRepository(options);

            var result = await repo.GetBrandByBrandCodeAsync("NOT_EXIST");

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Brand not found.");
        }
        #endregion
    }
}

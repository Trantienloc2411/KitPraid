using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repository;

namespace ProductService.Infrastructure.Test.Repositories
{
    [TestFixture]
    public class ImageRepositoryTests
    {
        private DbContextOptions<ProductDbContext> CreateOptions(string dbName) =>
            new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

        private ImageRepository CreateRepository(DbContextOptions<ProductDbContext> options)
        {
            var context = new ProductDbContext(options);
            return new ImageRepository(context);
        }

        private Image SeedImage(ProductDbContext context, Guid? productId = null)
        {
            var image = new Image
            {
                Id = Guid.NewGuid(),
                ImageName = "Test Image",
                ImagePath = "https://example.com/image.png",
                ProductId = productId ?? Guid.NewGuid(),
                IsActive = true,
                IsDeleted = false
            };
            context.Images.Add(image);
            context.SaveChanges();
            return image;
        }

        #region GetImageAsync
        [Test]
        public async Task GetImageAsync_ShouldReturnImage_WhenExists()
        {
            var options = CreateOptions("GetImage");
            var context = new ProductDbContext(options);
            var repo = new ImageRepository(context);
            var image = SeedImage(context);

            var result = await repo.GetImageAsync(image.Id);

            result.Success.Should().BeTrue();
            result.Data!.Id.Should().Be(image.Id);
        }

        [Test]
        public async Task GetImageAsync_ShouldFail_WhenNotExists()
        {
            var options = CreateOptions("GetImageNotExist");
            var repo = CreateRepository(options);

            var result = await repo.GetImageAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Image not found.");
        }
        #endregion

        #region DeleteImageAsync
        [Test]
        public async Task DeleteImageAsync_ShouldMarkImageDeleted_WhenExists()
        {
            var options = CreateOptions("DeleteImage");
            var context = new ProductDbContext(options);
            var repo = new ImageRepository(context);
            var image = SeedImage(context);

            var result = await repo.DeleteImageAsync(image.Id);

            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();

            var deletedImage = await context.Images.FindAsync(image.Id);
            deletedImage!.IsDeleted.Should().BeTrue();
        }

        [Test]
        public async Task DeleteImageAsync_ShouldFail_WhenNotExists()
        {
            var options = CreateOptions("DeleteImageNotExist");
            var repo = CreateRepository(options);

            var result = await repo.DeleteImageAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Image not found.");
        }
        #endregion

        #region UpdateImageAsync
        [Test]
        public async Task UpdateImageAsync_ShouldUpdateImageProperties()
        {
            var options = CreateOptions("UpdateImage");
            var context = new ProductDbContext(options);
            var repo = new ImageRepository(context);
            var image = SeedImage(context);

            image.ImageName = "Updated Name";
            image.IsActive = false;

            var result = await repo.UpdateImageAsync(image);

            result.Success.Should().BeTrue();
            result.Data!.ImageName.Should().Be("Updated Name");
            result.Data.IsActive.Should().BeFalse();

            var updatedImage = await context.Images.FindAsync(image.Id);
            updatedImage!.ImageName.Should().Be("Updated Name");
            updatedImage.IsActive.Should().BeFalse();
        }
        #endregion
    }
}
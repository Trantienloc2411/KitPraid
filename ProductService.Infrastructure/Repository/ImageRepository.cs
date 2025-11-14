using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repository;

public class ImageRepository(ProductDbContext productDbContext) : IImageRepository
{
    private readonly ProductDbContext _dbContext;

    public async Task<bool> DeleteImageAsync(Guid imageId)
    {
        var image = await _dbContext.Images.FindAsync(imageId);
        if (image == null)
        {
            return false;
        }
        else
        {
            image.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

    public async Task<Image> GetImageAsync(Guid imageId)
    {
        var image = await _dbContext.Images.FindAsync(imageId) ?? throw new InvalidOperationException($"Image with ID {imageId} not found.");
        return image;
    }

    public async Task<Image> UpdateImageAsync(Image image)
    {
        var existingImage = await _dbContext.Images.FindAsync(image.Id) ?? throw new InvalidOperationException($"Image with ID {image.Id} not found.");
        existingImage.ImagePath = image.ImagePath;
        existingImage.ProductId = image.ProductId;
        existingImage.IsDeleted = image.IsDeleted;

        await _dbContext.SaveChangesAsync();
        return existingImage;
    }
}
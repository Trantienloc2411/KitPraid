using ProductService.Domain;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repository;

public class ImageRepository(ProductDbContext productDbContext) : IImageRepository
{

    public async Task<OperationResult<bool>> DeleteImageAsync(Guid imageId)
    {
        var image = await productDbContext.Images.FindAsync(imageId);
        if (image == null)
        {
            return OperationResult<bool>.Fail("Image not found.");
        }
        else
        {
            image.IsDeleted = true;
            await productDbContext.SaveChangesAsync();
            return OperationResult<bool>.Ok(true);
        }
    }

    public async Task<OperationResult<Image>> GetImageAsync(Guid imageId)
    {
        var image = await productDbContext.Images.FindAsync(imageId);
        return image == null ? OperationResult<Image>.Fail("Image not found.") : OperationResult<Image>.Ok(image);
    }

    public async Task<OperationResult<Image>> UpdateImageAsync(Image image)
    {
        var result = productDbContext.Images.Update(image);
        await productDbContext.SaveChangesAsync();
        return OperationResult<Image>.Ok(image);
    }
}
using ProductService.Domain.Entities;

namespace ProductService.Domain.Repositories;

public interface IImageRepository
{
    public Task<bool> DeleteImageAsync(Guid imageId);
    public Task<Image>  GetImageAsync(Guid imageId);
    public Task<Image> UpdateImageAsync(Image image);
    
}
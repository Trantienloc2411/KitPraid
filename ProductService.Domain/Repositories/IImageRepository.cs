using ProductService.Domain;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Repositories;

public interface IImageRepository
{
    public Task<OperationResult<bool>> DeleteImageAsync(Guid imageId);
    public Task<OperationResult<Image>> GetImageAsync(Guid imageId);
    public Task<OperationResult<Image>> UpdateImageAsync(Image image);
}
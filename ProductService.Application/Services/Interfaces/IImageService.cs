using System;
using ProductService.Domain.Entities;

namespace ProductService.Application.Services.Interfaces;

public interface IImageService
{
    public Task<bool> DeleteImageAsync(Guid imageId);

}

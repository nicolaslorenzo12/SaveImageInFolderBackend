using SaveImageToRequiredFolder.Dto;
using SaveImageToRequiredFolder.Models;

namespace SaveImageToRequiredFolder.Service.Interfaces
{
    public interface IImageServiceInterface
    {
        Task AddImage(AddImageDto addImageDto);
    }
}

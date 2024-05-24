using SaveImageToRequiredFolder.Dto;

namespace SaveImageToRequiredFolder.Service.Interfaces
{
    public interface IImageService
    {
        Task AddImage(AddImageDto addImageDto);
    }
}

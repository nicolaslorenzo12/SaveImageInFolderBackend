using SaveImageToRequiredFolder.Dto;

namespace SaveImageToRequiredFolder.Service.Interfaces
{
    public interface IImageService
    {
        void AddImage(AddImageDto addImageDto);
    }
}

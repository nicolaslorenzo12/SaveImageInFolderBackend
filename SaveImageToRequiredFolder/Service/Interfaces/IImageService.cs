using SaveImageToRequiredFolder.Dto;
using SaveImageToRequiredFolder.Models;

namespace SaveImageToRequiredFolder.Service.Interfaces
{
    public interface IImageService
    {
        void AddImage(AddImageDto addImageDto);
        IReadOnlyCollection<Image> GetLastFivePictures(string folderName);
    }
}

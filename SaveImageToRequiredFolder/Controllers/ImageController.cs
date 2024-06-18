using Microsoft.AspNetCore.Mvc;
using SaveImageToRequiredFolder.Dto;
using SaveImageToRequiredFolder.Models;
using SaveImageToRequiredFolder.Service.Interfaces;

namespace SaveImageToRequiredFolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;
        private readonly IFolderService folderService;

        public ImageController(IImageService imageService, IFolderService folderService)
        {
            this.imageService = imageService;
            this.folderService = folderService;
        }

        [HttpPost]
        public ActionResult AddImage([FromBody] AddImageDto addImageDto)
        {
            imageService.AddImage(addImageDto);
            return Ok();
        }

        [HttpGet("{folderName}")]
        public IReadOnlyCollection<Image> GetLast5Images(string folderName)
        {
            bool folderExists = folderService.FolderExists(folderName);

            if (folderExists)
            {
                return imageService.GetLastFivePictures(folderName);
            }
            else
            {
                return [];
            }
        }
    }
}

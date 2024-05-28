using Microsoft.AspNetCore.Mvc;
using SaveImageToRequiredFolder.Dto;
using SaveImageToRequiredFolder.Service.Interfaces;

namespace SaveImageToRequiredFolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        public ActionResult AddImage([FromBody] AddImageDto addImageDto)
        {
            imageService.AddImage(addImageDto);
            return Ok();
        }
    }
}

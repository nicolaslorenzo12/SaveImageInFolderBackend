using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveImageToRequiredFolder.Dto;
using SaveImageToRequiredFolder.Service.Interfaces;

namespace SaveImageToRequiredFolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageServiceInterface imageService;

        public ImageController(IImageServiceInterface imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        public async Task<ActionResult> AddImage([FromBody] AddImageDto addImageDto)
        {
            await imageService.AddImage(addImageDto);
            return Ok();
        }
    }
}

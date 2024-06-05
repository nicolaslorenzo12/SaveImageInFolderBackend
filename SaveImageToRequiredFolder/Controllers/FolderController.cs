using Microsoft.AspNetCore.Mvc;
using SaveImageToRequiredFolder.Service.Interfaces;

namespace SaveImageToRequiredFolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService folderService;

        public FolderController(IFolderService folderService)
        {
            this.folderService = folderService;
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<string>> GetFolders()
        {
            try
            {
                IReadOnlyCollection<string> folders = folderService.ReadAllFolders();
                return Ok(folders);
            }
            catch (DirectoryNotFoundException)
            {
                return NotFound("Base direcory does not exist yet");
            }
}

        [HttpGet("folderExists/{folderName}")]
        public bool FolderExists(string folderName)
        {
            return folderService.FolderExists(folderName);
        }

    }
}

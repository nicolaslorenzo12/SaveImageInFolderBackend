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
            IReadOnlyCollection<string> folders = folderService.ReadAllFolders();
            return Ok(folders);
        }

        [HttpGet("folderExists/{folderName}")]
        public bool FolderExists(string folderName)
        {
            return folderService.FolderExists(folderName);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using SaveImageToRequiredFolder.Models;
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
        public async Task<ActionResult<IReadOnlyCollection<string>>> GetFolders()
        {
            IReadOnlyCollection<Folder> folders = await folderService.ReadAllFolders();
            IReadOnlyCollection<string> folderNames = folders.Select(folder => folder.name).ToList().AsReadOnly();
            return Ok(folderNames);
        }

        [HttpGet("folderExists/{folderName}")]
        public async Task<bool> FolderExists(string folderName)
        {
            return await folderService.FolderExists(folderName);
        }

    }
}

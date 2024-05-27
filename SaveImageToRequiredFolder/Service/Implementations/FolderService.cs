using Microsoft.EntityFrameworkCore;
using SaveImageToRequiredFolder.Data;
using SaveImageToRequiredFolder.Models;
using SaveImageToRequiredFolder.Service.Interfaces;

namespace SaveImageToRequiredFolder.Service.Implementations
{
    public class FolderService : IFolderService
    {

        private readonly Context context;

        public FolderService(Context context)
        {
            this.context = context;
        }

        public async Task<bool> FolderExists(string folderName)
        {
            return await context.Folders.AnyAsync(f => f.name == folderName);
        }

        public async Task<IReadOnlyCollection<Folder>> ReadAllFolders()
        {
            return await context.Folders.ToListAsync();
        }
    }
}

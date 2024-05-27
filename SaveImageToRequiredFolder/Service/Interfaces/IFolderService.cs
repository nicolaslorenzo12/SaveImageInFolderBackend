using SaveImageToRequiredFolder.Models;

namespace SaveImageToRequiredFolder.Service.Interfaces
{
    public interface IFolderService
    {
        Task<IReadOnlyCollection<Folder>> ReadAllFolders();
        Task<bool> FolderExists(string folderName);
    }
}

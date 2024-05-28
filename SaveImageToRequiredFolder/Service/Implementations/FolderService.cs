using SaveImageToRequiredFolder.Models;
using SaveImageToRequiredFolder.Service.Interfaces;

namespace SaveImageToRequiredFolder.Service.Implementations
{
    public class FolderService : IFolderService
    {
        private static readonly string baseDirectory = @"C:\mypictures";

        public FolderService()
        {
        }

        public async Task<bool> FolderExists(string folderName)
        {
            string folderPath = Path.Combine(baseDirectory, folderName);
            bool exists = Directory.Exists(folderPath);
            return await Task.FromResult(exists);
        }

        public async Task<IReadOnlyCollection<Folder>> ReadAllFolders()
        {
            var folderNames = Directory.GetDirectories(baseDirectory)
                                        .Select(dir => Path.GetFileName(dir))
                                        .ToList()
                                        .AsReadOnly();
            return (IReadOnlyCollection<Folder>)await Task.FromResult<IReadOnlyCollection<string>>(folderNames);
        }
    }
}

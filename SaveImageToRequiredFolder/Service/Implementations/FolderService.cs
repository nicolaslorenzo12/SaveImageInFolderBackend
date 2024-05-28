using SaveImageToRequiredFolder.Service.Interfaces;

namespace SaveImageToRequiredFolder.Service.Implementations
{
    public class FolderService : IFolderService
    {
        private static readonly string baseDirectory = @"C:\mypictures";

        public FolderService()
        {
        }

        public bool FolderExists(string folderName)
        {
            string folderPath = Path.Combine(baseDirectory, folderName);
            bool exists = Directory.Exists(folderPath);
            return exists;
        }

        public IReadOnlyCollection<string> ReadAllFolders()
        {
            return Directory.GetDirectories(baseDirectory)
                                .Select(dir => Path.GetFileName(dir))
                                    .ToList()
                                        .AsReadOnly();
        }
    }
}

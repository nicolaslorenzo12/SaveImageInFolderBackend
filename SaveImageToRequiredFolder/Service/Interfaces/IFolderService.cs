namespace SaveImageToRequiredFolder.Service.Interfaces
{
    public interface IFolderService
    {
        IReadOnlyCollection<String> ReadAllFolders();
        bool FolderExists(string folderName);
    }
}

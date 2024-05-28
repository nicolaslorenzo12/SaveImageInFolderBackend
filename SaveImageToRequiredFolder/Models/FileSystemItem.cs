namespace SaveImageToRequiredFolder.Models
{
    public class FileSystemItem
    {
        public FileSystemItem(string name)
        {
            this.name = name;
        }
        public string name { get; set; }
    }
}
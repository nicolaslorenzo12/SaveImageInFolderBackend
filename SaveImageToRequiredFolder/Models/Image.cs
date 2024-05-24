namespace SaveImageToRequiredFolder.Models
{
    public class Image
    {
        public Image(string fileName, byte[] data, string folderName)
        {
            this.FileName = fileName;
            this.Data = data;
            this.FolderName = folderName;
        }

        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string FolderName { get; set; }
    }
}

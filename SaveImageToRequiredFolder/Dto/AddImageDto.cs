namespace SaveImageToRequiredFolder.Dto
{
    public class AddImageDto
    {
        public AddImageDto(string fileName, string folderName, string imageData)
        {
            this.fileName = fileName;
            this.folderName = folderName;
            this.imageData = imageData;
        }

        public string fileName { get;}
        public string folderName { get; }
        public string imageData { get; }
    }
}

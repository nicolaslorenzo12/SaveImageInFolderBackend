namespace SaveImageToRequiredFolder.Dto
{
    public class AddImageDto
    {
        public AddImageDto(string folderName, string imageData)
        {
            this.folderName = folderName;
            this.imageData = imageData;
        }

        public string folderName { get; }
        public string imageData { get; }
    }
}

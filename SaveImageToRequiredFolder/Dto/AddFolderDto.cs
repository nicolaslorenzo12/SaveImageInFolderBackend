namespace SaveImageToRequiredFolder.Dto
{
    public class AddFolderDto
    {
        public AddFolderDto(string name)
        {
            this.name = name;
        }

        public string name { get; }
    }
}

using SaveImageToRequiredFolder.Dto;
using SaveImageToRequiredFolder.Service.Interfaces;
namespace SaveImageToRequiredFolder.Service.Implementations
{
    public class ImageService : IImageService
    {

        public ImageService()
        {
        }

        public async Task AddImage(AddImageDto addImageDto)
        {
            string fileName = null;
            byte[] imageData = Convert.FromBase64String(addImageDto.imageData);
            string baseDirectory = @"C:\mypictures";

            await CheckIfDirectoryExistsAndIfNotCreateIt(baseDirectory);
            string folderPath = Path.Combine(baseDirectory, addImageDto.folderName);

            await CheckIfDirectoryExistsAndIfNotCreateIt(folderPath);
            await GiveProperFileNameToImage(addImageDto.folderName, folderPath ,fileName, imageData);
        }

        private async Task CheckIfDirectoryExistsAndIfNotCreateIt(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
               Directory.CreateDirectory(folderPath);
            }
        }

        private async Task GiveProperFileNameToImage(string folderName,string folderPath ,string fileName,byte[] imageData)
        {
            int maxNumber = 0;

            // Get all files in the folder
            string[] files = Directory.GetFiles(folderPath, "picture(*).jpg");
            foreach (string file in files)
            {
                // Extract the number from the filename
                string fileNameWithJPg = Path.GetFileNameWithoutExtension(file);
                if (fileNameWithJPg.StartsWith("picture(") && fileNameWithJPg.EndsWith(")"))
                {
                    string numberString = fileNameWithJPg.Substring(8, fileNameWithJPg.Length - 9);
                    if (int.TryParse(numberString, out int number))
                    {
                        if (number > maxNumber)
                        {
                            maxNumber = number;
                        }
                    }
                }
            }

            fileName = $"picture({maxNumber + 1}).jpg";

            await SetFinalNameToImageAndPutItInFolder(folderPath ,fileName, imageData);
        }

        private async Task SetFinalNameToImageAndPutItInFolder(string folderPath, string fileName, byte[] imageData)
        {
            string imagePath = Path.Combine(folderPath, fileName);
            await File.WriteAllBytesAsync(imagePath, imageData);
        }
    }
}

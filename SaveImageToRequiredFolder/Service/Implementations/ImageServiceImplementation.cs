using SaveImageToRequiredFolder.Models;
using SaveImageToRequiredFolder.Service.Interfaces;
using SaveImageToRequiredFolder.Data;
using SaveImageToRequiredFolder.Dto;
using System.Runtime.InteropServices;
namespace SaveImageToRequiredFolder.Service.Implementations
{
    public class ImageServiceImplementation : IImageServiceInterface
    {

        private readonly Context context;

        public ImageServiceImplementation(Context context)
        {
            this.context = context;
        }


        public async Task AddImage(AddImageDto addImageDto)
        {
            string fileName = null;
            byte[] imageData = Convert.FromBase64String(addImageDto.imageData);
            string baseDirectory = @"C:\mypictures";

            checkIfDirectoryExistsAndIfNotCreateIt(baseDirectory);
            // Combine the base directory with the folder name
            string folderPath = Path.Combine(baseDirectory, addImageDto.folderName);

            checkIfDirectoryExistsAndIfNotCreateIt(folderPath);
            giveProperFileNameToImage(addImageDto, folderPath, fileName, imageData);
        }

        private void checkIfDirectoryExistsAndIfNotCreateIt(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                 Directory.CreateDirectory(folderPath);
            }
        }

        private async void giveProperFileNameToImage(AddImageDto addImageDto, string folderPath, string fileName, byte[] imageData)
        {
            if (string.IsNullOrEmpty(addImageDto.fileName))
            {
                giveNameToImageByCounting(folderPath, fileName, imageData);
            }
            else
            {
                giveGivenNameToImage(folderPath, fileName, imageData, addImageDto);
            }
        }

        private async Task setFinalNameToImageAndPutItInFolder(string folderPath, string fileName, byte[] imageData)
        {
            string imagePath = Path.Combine(folderPath, fileName);
            await File.WriteAllBytesAsync(imagePath, imageData);
        }

        private async void giveNameToImageByCounting(string folderPath, string fileName, byte[] imageData)
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

            // Set the fileName to picture(number + 1).jpg
            fileName = $"picture({maxNumber + 1}).jpg";

            await setFinalNameToImageAndPutItInFolder(folderPath, fileName, imageData);
        }

        private async void giveGivenNameToImage(string folderPath, string fileName, byte[] imageData, AddImageDto addImageDto)
        {
            if (!addImageDto.fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            {
                fileName += addImageDto.fileName + ".jpg";
            }
            await setFinalNameToImageAndPutItInFolder(folderPath, fileName, imageData);
        }
    }
}

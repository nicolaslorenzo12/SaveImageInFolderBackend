using Microsoft.EntityFrameworkCore;
using SaveImageToRequiredFolder.Data;
using SaveImageToRequiredFolder.Dto;
using SaveImageToRequiredFolder.Models;
using SaveImageToRequiredFolder.Service.Interfaces;
namespace SaveImageToRequiredFolder.Service.Implementations
{
    public class ImageService : IImageService
    {

        private readonly Context context;

        public ImageService(Context context)
        {
            this.context = context;
        }


        public async Task AddImage(AddImageDto addImageDto)
        {
            string fileName = null;
            byte[] imageData = Convert.FromBase64String(addImageDto.imageData);
            string baseDirectory = @"C:\mypictures";

            await checkIfDirectoryExistsAndIfNotCreateIt(baseDirectory);
            // Combine the base directory with the folder name
            string folderPath = Path.Combine(baseDirectory, addImageDto.folderName);

            await checkIfDirectoryExistsAndIfNotCreateIt(folderPath);
            await giveProperFileNameToImage(addImageDto, folderPath, fileName, imageData);
        }

        private async Task checkIfDirectoryExistsAndIfNotCreateIt(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
               Directory.CreateDirectory(folderPath);
            }
        }

        private async Task giveProperFileNameToImage(AddImageDto addImageDto, string folderPath, string fileName, byte[] imageData)
        {
            if (string.IsNullOrEmpty(addImageDto.fileName))
            {
                await giveNameToImageByCounting(folderPath, fileName, imageData);
            }
            else
            {
                await giveGivenNameToImage(folderPath, fileName, imageData, addImageDto);
            }
        }

        private async Task setFinalNameToImageAndPutItInFolder(string folderPath, string fileName, byte[] imageData)
        {
            string imagePath = Path.Combine(folderPath, fileName);
            await System.IO.File.WriteAllBytesAsync(imagePath, imageData);

            try
            {
                var existingFolder = await context.Folders.FirstOrDefaultAsync(f => f.name == folderPath);

                if (existingFolder == null)
                {
                    var newFolder = new Models.Folder(folderPath);
                    context.Folders.Add(newFolder);
                }
                await context.SaveChangesAsync();

                // Now get the folder ID (whether it was just added or already existed)
                int folderId = (int)await context.Folders.Where(f => f.name == folderPath).Select(f => (int?)f.id).FirstOrDefaultAsync();
                bool existingFile= await context.Files.AnyAsync(f => f.name == fileName && f.folderId == folderId);

                // Add the file to the database
                if (!existingFile)
                {
                    var newFile = new Models.File(fileName, folderId);
                    context.Files.Add(newFile);
                }
                await context.SaveChangesAsync(); // Save changes for file addition
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Handle the exception appropriately
            }
        }

        private async Task giveNameToImageByCounting(string folderPath, string fileName, byte[] imageData)
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

        private async Task giveGivenNameToImage(string folderPath, string fileName, byte[] imageData, AddImageDto addImageDto)
        {
            if (!addImageDto.fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            {
                fileName += addImageDto.fileName + ".jpg";
            }
            await setFinalNameToImageAndPutItInFolder(folderPath, fileName, imageData);
        }
    }
}

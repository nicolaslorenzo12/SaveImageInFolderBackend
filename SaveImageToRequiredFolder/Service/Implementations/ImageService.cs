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

        private async Task GiveProperFileNameToImage(string folderName,string folderPath ,string fileName, byte[] imageData)
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

            await SetFinalNameToImageAndPutItInFolder(folderName, folderPath ,fileName, imageData);
        }

        private async Task SetFinalNameToImageAndPutItInFolder(string folderName, string folderPath, string fileName, byte[] imageData)
        {
            string imagePath = Path.Combine(folderPath, fileName);
            await System.IO.File.WriteAllBytesAsync(imagePath, imageData);

            try
            {
                Folder existingFolder = await context.Folders.FirstOrDefaultAsync(f => f.name == folderName);

               await AddFolderIfItDoesNotExist(existingFolder, folderName);

                int folderId = (int)await context.Folders.Where(f => f.name == folderName).Select(f => (int?)f.id).FirstOrDefaultAsync();

                await AddFile(fileName, folderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // I have to hand exceptions better
            }
        }

        private async Task AddFolderIfItDoesNotExist(Folder existingFolder, string folderName)
        {
            if (existingFolder == null)
            {
                Folder newFolder = new Models.Folder(folderName);
                context.Folders.Add(newFolder);
                await context.SaveChangesAsync();
            }           
        }

        private async Task AddFile(string fileName, int folderId)
        {
                var newFile = new Models.File(fileName, folderId);
                context.Files.Add(newFile);
                await context.SaveChangesAsync();    
        }
    }
}

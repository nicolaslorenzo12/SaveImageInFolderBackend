using SaveImageToRequiredFolder.Dto;
using SaveImageToRequiredFolder.Models;
using SaveImageToRequiredFolder.Service.Interfaces;
namespace SaveImageToRequiredFolder.Service.Implementations
{
    public class ImageService : IImageService
    {
        public ImageService()
        {
        }

        public void AddImage(AddImageDto addImageDto)
        {
            string imageCode = addImageDto.imageData.Substring(addImageDto.imageData.IndexOf(',') + 1);
            byte[] imageData = Convert.FromBase64String(imageCode);
            string baseDirectory = @"C:\mypictures";

            CheckIfDirectoryExistsAndIfNotCreateIt(baseDirectory);
            string folderPath = Path.Combine(baseDirectory, addImageDto.folderName);

            CheckIfDirectoryExistsAndIfNotCreateIt(folderPath);
            GiveProperFileNameToImage(folderPath ,imageData);
        }

        private void CheckIfDirectoryExistsAndIfNotCreateIt(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
               Directory.CreateDirectory(folderPath);
            }
        }

        private void GiveProperFileNameToImage(string folderPath ,byte[] imageData)
        {
            string[] files = Directory.GetFiles(folderPath, "picture(*).jpg");
            int maxNumber = FindAmountOfImagesInTheFolder(files);
            string fileName = $"picture({maxNumber + 1}).jpg";
            string imagePath = Path.Combine(folderPath, fileName);
            System.IO.File.WriteAllBytesAsync(imagePath, imageData);
        }

        private int FindAmountOfImagesInTheFolder(string[] files)
        {
            int maxNumber = 0;

            foreach (string file in files)
            {
                string fileNameWithJpg = Path.GetFileNameWithoutExtension(file);
                if (fileNameWithJpg.StartsWith("picture(") && fileNameWithJpg.EndsWith(")"))
                {
                    string numberString = fileNameWithJpg.Substring(8, fileNameWithJpg.Length - 9);
                    if (int.TryParse(numberString, out int number))
                    {
                        if (number > maxNumber)
                        {
                            maxNumber = number;
                        }
                    }
                }
            }

            return maxNumber;
        }

        public IReadOnlyCollection<Image> GetLastFivePictures(string folderName)
        {
            string baseDirectory = @"C:\mypictures";
            string folderPath = Path.Combine(baseDirectory, folderName);

            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException($"The folder {folderName} does not exist.");
            }

            string[] files = Directory.GetFiles(folderPath, "picture(*).jpg");

            var sortedFiles = files
                .Select(file => new
                {
                    FilePath = file,
                    FileNumber = ExtractNumberFromFileName(Path.GetFileNameWithoutExtension(file))
                })
                .Where(file => file.FileNumber.HasValue)
                .OrderByDescending(file => file.FileNumber.Value)
                .Take(5)
                .ToList();

            List<Image> images = [];

            foreach (var file in sortedFiles)
            {
                byte[] imageData = System.IO.File.ReadAllBytes(file.FilePath);
                images.Add(new Image(Path.GetFileName(file.FilePath), imageData, folderName));
            }

            return images;
        }

        private int? ExtractNumberFromFileName(string fileName)
        {
            if (fileName.StartsWith("picture(") && fileName.EndsWith(")"))
            {
                string numberString = fileName.Substring(8, fileName.Length - 9);
                if (int.TryParse(numberString, out int number))
                {
                    return number;
                }
            }

            return null;
        }

    }
}

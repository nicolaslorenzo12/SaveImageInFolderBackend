using SaveImageToRequiredFolder.Dto;
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
            byte[] imageData = Convert.FromBase64String(addImageDto.imageData);
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
            File.WriteAllBytesAsync(imagePath, imageData);
        }

        private int FindAmountOfImagesInTheFolder(string[] files)
        {
            int maxNumber = 0;

            foreach (string file in files)
            {
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
            return maxNumber;
        }
    }
}

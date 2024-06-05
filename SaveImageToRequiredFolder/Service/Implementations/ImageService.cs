﻿using SaveImageToRequiredFolder.Dto;
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
            File.WriteAllBytesAsync(imagePath, imageData);
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
    }
}

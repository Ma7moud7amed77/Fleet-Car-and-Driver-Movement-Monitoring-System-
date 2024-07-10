using System;
using System.IO;
namespace TestGP
{
    public class convertImage
    {

        public  byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] image = null;

            // Check if the image file exists
            if (File.Exists(imagePath))
            {
                // Read the image file into a byte array
                image = File.ReadAllBytes(imagePath);
            }
            else
            {
                Console.WriteLine("Image file does not exist.");
            }

            return image;
        }
    }
}

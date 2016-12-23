using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ImageResizer
{
    public static class ImageResizer
    {
        public static void ResizeImages(int containerHeight, int containerWidth, string directory)
        {
            var paths = GetImagePaths(directory);

            foreach (var path in paths)
            {
                var fileName = Path.GetFileName(path);
                var newImage = ScaleImage(Image.FromFile(path), containerWidth, containerHeight);
                SaveImage(newImage, directory, fileName);
            }
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private static IEnumerable<string> GetImagePaths(string directory)
        {
            var acceptableExtensions = new List<string> {".jpg", ".png"};

            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory);
                var extension = Path.GetExtension(files[0]);
                return files.Where(x => acceptableExtensions.Contains(Path.GetExtension(x)));
            }

            return new List<string>();
        }

        private static void SaveImage(Image image, string directory, string fileName)
        {
            if (!Directory.Exists($"{directory}\\Resized"))
            {
                Directory.CreateDirectory($"{directory}\\Resized");
            }

            image.Save($"{directory}\\Resized\\{fileName}");
        }
    }
}
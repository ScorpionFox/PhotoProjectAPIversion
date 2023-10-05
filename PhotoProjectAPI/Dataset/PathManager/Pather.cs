﻿namespace PhotoProjectAPI.Dataset.PathManager
{
    public class Pather
    {
        public static string GetCurrentDirectory()
        {
            var result = Directory.GetCurrentDirectory();
            return result;
        }

        public static string GetStaticContentDirectory()
        {
            var result = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Dataset\\Images\\");
            if (!Directory.Exists(result))
                Directory.CreateDirectory(result);
            return result;
        }

        public static string GetFilePath(string fileName)
        {
            var getStaticContentDirectory = GetStaticContentDirectory();
            var result = System.IO.Path.Combine(getStaticContentDirectory, fileName);
            return result;
        }





    }
}

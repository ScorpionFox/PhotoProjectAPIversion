namespace PhotoProjectAPI.Helper
{
    public class CommonPath
    {
        // Pobiera bieżący katalog roboczy aplikacji.
        public static string GetCurrentDirectory()
        {
            var result = Directory.GetCurrentDirectory();
            return result;
        }

        // Pobiera katalog, w którym przechowywane są pliki statyczne (np. zdjęcia).
        // Jeśli katalog nie istnieje, zostaje utworzony.
        public static string GetStaticContentDirectory()
        {
            // Pobiera bieżący katalog roboczy aplikacji.
            var result = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data\\Photos\\");

            // Jeśli katalog nie istnieje, zostaje utworzony.
            if (!Directory.Exists(result))
                Directory.CreateDirectory(result);

            return result;
        }

        // Pobiera pełną ścieżkę do określonego pliku w katalogu statycznym.
        public static string GetFilePath(string fileName)
        {
            // Pobiera katalog, w którym przechowywane są pliki statyczne (np. zdjęcia).
            var getStaticContentDirectory = GetStaticContentDirectory();

            // Łączy ścieżkę do katalogu statycznego z nazwą pliku.
            var result = System.IO.Path.Combine(getStaticContentDirectory, fileName);
            return result;
        }
    }
}
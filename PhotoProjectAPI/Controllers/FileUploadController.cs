using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnviroment;

        public FileUploadController(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
        }


        // POST
        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload fileUpload)
        {
            try
            {
                if (fileUpload.files.Length > 0)
                {
                    string path = _webHostEnviroment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream=System.IO.File.Create(path+ fileUpload.files.FileName))
                    {
                        fileUpload.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "Upload Done.";
                    }
                }
                else
                {
                    return "Failed.";
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        // GET
        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get([FromRoute] string fileName)
        {
            string path = _webHostEnviroment.WebRootPath + "\\uploads\\";
            var filePath = path + fileName;

            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

                // Określ ContentType na podstawie rozszerzenia pliku
                string contentType = "application/octet-stream"; // Domyślny ContentType

                // Dodaj obsługę różnych rozszerzeń plików
                if (fileExtension == ".png")
                {
                    contentType = "image/png";
                }
                else if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                {
                    contentType = "image/jpeg";
                }
                else if (fileExtension == ".gif")
                {
                    contentType = "image/gif";
                }
                // Dodaj obsługę innych rozszerzeń, jeśli jest to konieczne

                return File(b, contentType);
            }

            return NotFound(); // Możesz zwrócić NotFound, jeśli plik nie istnieje
        }
    }
}

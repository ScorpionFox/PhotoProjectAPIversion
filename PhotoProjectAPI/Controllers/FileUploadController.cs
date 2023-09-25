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
                if (fileUpload.Files.Length > 0)
                {
                    string path = _webHostEnviroment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream=System.IO.File.Create(path+ fileUpload.Files.FileName))
                    {
                        fileUpload.Files.CopyTo(fileStream);
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
            var filePath = path + fileName + ".png";
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                // POBIERA NARAZIE TYLKO W FORMACIE PNG !!!!
                return File(b, "image/png");
            }
            return null;
        }
    }
}

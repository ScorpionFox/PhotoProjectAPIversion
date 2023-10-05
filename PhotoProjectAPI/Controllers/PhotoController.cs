using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PhotoProjectAPI.Dataset.VM;
using static System.Net.WebRequestMethods;
using System.Reflection.Metadata;
using System;

namespace PhotoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private static IWebHostEnvironment _webHostEnvironment;
        public PhotoController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("AddPhoto")]
        public async Task<string> AddPhoto([FromForm] PhotoViewmodel photo)
        {
            try
            {
                if (photo.ImageFile.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + photo.ImageFile.FileName))
                    {
                        photo.ImageFile.CopyTo(fileStream);
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

        [HttpGet("GetPhoto/{fileName}")]
        public async Task<IActionResult> GetPhoto([FromRoute] string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            var filePath = path + fileName;

            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/force-download", fileName);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("UpdatePhoto")]
        public async Task<IActionResult> UpdatePhoto([FromForm] PhotoUpdateViewmodel photo)
        {
            
            return Ok();
        }

        [HttpDelete("DeletePhoto")]
        public async Task<IActionResult> DeletePhoto([FromForm] PhotoUpdateViewmodel photo)
        {
            return Ok();
        }
    }
}
        
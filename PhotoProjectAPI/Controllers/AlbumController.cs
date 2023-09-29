using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.Dataset.VM;

namespace PhotoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        [HttpGet("GetAlbum/{id}")]
        public async Task<IActionResult> GetAlbum([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost("AddAlbum")]
        public async Task<IActionResult> AddAlbum([FromBody] AlbumViewmodel album)
        {
            return Ok();
        }

        [HttpPut("UpdateAlbum")]
        public async Task<IActionResult> UpdateAlbum([FromBody] AlbumViewmodel album)
        {
            return Ok();
        }

        [HttpDelete("DeleteAlbum/{id}")]
        public async Task<IActionResult> DeleteAlbum([FromRoute] int id)
        {
            return Ok();
        }
    }
}
        
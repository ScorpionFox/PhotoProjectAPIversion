using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.Data.Interfaces;
using PhotoProjectAPI.Dataset.VM;

namespace PhotoProjectAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAlbum(int? albumId = null, string? accessNumber = null)
        {
            var album = await _albumService.GetAlbumAsync(albumId,accessNumber);
            if (album == null) return NotFound();

            return new JsonResult(album);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddAlbum([FromBody] AlbumViewmodel album)
        {
            return Ok();
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> UpdateAlbum([FromBody] AlbumViewmodel album)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAlbum([FromRoute] int id)
        {
            return Ok();
        }
    }
}
        
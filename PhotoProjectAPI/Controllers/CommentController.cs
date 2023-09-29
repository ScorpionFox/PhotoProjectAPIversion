using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        [HttpGet("GetComments/{photoId}")]
        public async Task<IActionResult> GetComments([FromRoute] int photoId)
        {
            return Ok();
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentsViewmodel comment)
        {
            return Ok();
        }

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment([FromBody] CommentsViewmodel comment) // za pomocą id
        {
            return Ok();
        }

        [HttpDelete("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            return Ok();
        }
    }
}

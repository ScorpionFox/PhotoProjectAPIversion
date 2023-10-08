using PhotoProjectAPI.Data;
using PhotoProjectAPI.Data.Interfaces;
using PhotoProjectAPI.Data.Services;
using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace PhotoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public CommentService _commentService;
        public PhotoService _photoService;
        public CommentController(CommentService commentService, PhotoService photoService)
        {
            _commentService = commentService;
            _photoService = photoService;
        }
        [HttpGet("GetComments/{photoId}")]
        public IActionResult GetCommentsByPhotoId(int photoId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("ADMIN");
            if (_photoService.PhotoExists(photoId) == false || _photoService.HasAccess(photoId, userId, isAdmin) == false)
                return NotFound();
            else
            {
                var obj = _commentService.GetAllCommentsByPhoto(photoId);
                return Ok(obj);
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpGet("DeleteComment/{commentId}")]
        public IActionResult DeleteCommentById(int commentId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("ADMIN");

            if (_commentService.CommentExists(commentId) == false)
                return NotFound();
            else if (_commentService.HasPriveleges(commentId, userId, isAdmin) == false)
                return Forbid();
            else
            {
                _commentService.DeleteCommentById(commentId);
                return Ok();
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPost("AddComment/{photoId}")]
        public IActionResult AddCommentToPhoto(int photoId, CommentVM comment)
        {
            var photo = _photoService.GetPhotoById(photoId);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("ADMIN");

            if (photo == null || string.IsNullOrWhiteSpace(comment.Comment) == true)
                return BadRequest();
            else
            {
                bool hasAccess = _photoService.HasAccess(photoId, userId, isAdmin);
                bool isAuthor = _commentService.IsAuthor(photoId, userId, isAdmin);
                if (isAuthor || hasAccess == false)
                    return Forbid();
                else
                {
                    _commentService.AddComment(comment, userId, photoId);
                    return Ok();
                }
            }
        }
    }
}

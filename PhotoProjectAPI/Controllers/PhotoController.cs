using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.Data.Services;
using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Models;
using PhotoProjectAPI.Data.Interfaces;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;
using PhotoProjectAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace PhotoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private PhotoService _photoService;
        private static IWebHostEnvironment _webHostEnvironment;
        public PhotoController(PhotoService photoService, IWebHostEnvironment webHostEnvironment)
        {
            _photoService = photoService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("get-all-photos")]
        public IActionResult GetPhotos()
        {
            var allPhotos = _photoService.GetAllPhotos();
            bool isAdmin = User.IsInRole("ADMIN");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (isAdmin)
                allPhotos = allPhotos;
            else
                allPhotos = allPhotos.Where(p => p.Access == Data.AccessLevel.Public || p.UserId == userId).ToList();

            return Ok(allPhotos);
        }

        [HttpGet("get-photo-by-id/{photoId}")]
        public IActionResult GetPhotoById(int photoId)
        {
            var photo = _photoService.GetPhotoById(photoId);
            if (photo == null)
                return NotFound();
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                bool isAdmin = User.IsInRole("ADMIN");

                if (!_photoService.HasAccess(photoId, userId, isAdmin))
                    return Forbid();
                return Ok(photo);
            }

        }
        [HttpGet("get-photo(s)-by-name")]
        public IActionResult GetPhotosByName(string photoName) 
        {
            bool isAdmin = User.IsInRole("ADMIN");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var filteredPhotos = _photoService.GetPhotosByName(photoName);

            if (filteredPhotos.Count == 0)
                return NotFound();
            else
            {
                if (isAdmin)
                    filteredPhotos = filteredPhotos;
                else
                    filteredPhotos = filteredPhotos.Where(p => p.Access == Data.AccessLevel.Public || p.UserId == userId).ToList();
            }


            return Ok(filteredPhotos);
        }
        [HttpGet("get-photos-by-user-name/{userName}")]
        public IActionResult GetPhotosByUserName(string userName)
        {
            bool isAdmin = User.IsInRole("ADMIN");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var filteredPhotos = _photoService.GetPhotosByAuthorName(userName);

            if (filteredPhotos == null)
                return NotFound();
            else if (filteredPhotos.Count == 0)
                return NotFound();
            else
            {
                if (isAdmin)
                    filteredPhotos = filteredPhotos;
                else
                    filteredPhotos = filteredPhotos.Where(p => p.Access == Data.AccessLevel.Public || p.UserId == userId).ToList();

                return Ok(filteredPhotos);
            }
        }
        [HttpGet("get-photos-by-user-id/{authorId}")]
        public IActionResult GetPhotosByUserId(string authorId)
        {
            bool isAdmin = User.IsInRole("ADMIN");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var filteredPhotos = _photoService.GetPhotosByAuthorId(authorId);

            if (filteredPhotos.Count == 0)
                return NotFound();
            else
            {
                if (isAdmin)
                    filteredPhotos = filteredPhotos;
                else
                    filteredPhotos = filteredPhotos.Where(p => p.Access == Data.AccessLevel.Public || p.UserId == userId).ToList();

                return Ok(filteredPhotos);
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPost("add-photo")]
        public async Task<IActionResult> AddPhoto([FromForm] PhotoVM photo)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _photoService.AddPhoto(photo, userId);
            return Ok(photo);
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPost("give-upvote/{photoId}")]
        public IActionResult GiveUpvote(int photoId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var voteDetails = _photoService.GetVoteDetails(userId, photoId);

            if (_photoService.PhotoExists(photoId) == false)
                return NotFound();
            else if (voteDetails == true)
            {
                _photoService.AddUpvote(photoId);
                return Ok();
            }
            else
                return Forbid();
        }

        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPost("give-downvote/{photoId}")]
        public IActionResult GiveDownvote(int photoId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var voteDetails = _photoService.GetVoteDetails(userId, photoId);

            if (_photoService.PhotoExists(photoId) == false)
                return NotFound();
            else if (voteDetails == true)
            {
                _photoService.AddDownvote(photoId);
                return Ok();
            }
            else
                return Forbid();
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPut("update-photo-by-id/{photoId}")]
        public IActionResult UpdatePhotoById(int photoId, [FromForm] PhotoUpdateVM photo)
        {
            string userId = _photoService.GetUserIdByPhotoId(photoId);
            bool isAdmin = User.IsInRole("ADMIN");
            string currUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!_photoService.PhotoExists(photoId))
            {
                return NotFound();
            }
            else
            {
                if (_photoService.HasPriveleges(photoId, currUserId, isAdmin))
                {
                    var photoUpd = _photoService.UpdatePhotoById(photoId, photo, userId);
                    return Ok(photoUpd); ;
                }
                else
                    return Forbid();
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPut("change-photo-access-level")]
        public IActionResult ChangeAccess(int photoId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("ADMIN");

            if (!_photoService.PhotoExists(photoId) == false)
                return NotFound();
            else if (_photoService.HasPriveleges(photoId, userId, isAdmin))
                return Ok(_photoService.ChangeAccessById(photoId));
            else
                return Forbid();
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpDelete("delete-photo-by-id/{photoId}")]
        public IActionResult DeletePhotoById(int photoId)
        {
            bool isAdmin = User.IsInRole("ADMIN");
            string currUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!_photoService.PhotoExists(photoId))
            {
                return NotFound();
            }
            else
            {
                if (_photoService.HasPriveleges(photoId, currUserId, isAdmin))
                {
                    _photoService.DeletePhotoById(photoId);
                    return Ok();
                }
                else
                    return Forbid();
            }
        }          
    }
}

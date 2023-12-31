﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.Data.Services;
using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PhotoProjectAPI.Data.Interfaces;
using PhotoProjectAPI.Data;


namespace PhotoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        public AlbumService _albumService;
        public AlbumController(AlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAlbums()
        {
            var allAlbums = _albumService.GetAllAlbums();
            bool isAdmin = User.IsInRole("ADMIN");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (isAdmin)
                allAlbums = allAlbums;
            else
                allAlbums = allAlbums.Where(p => p.Access == Data.AccessLevel.Public || p.UserId == userId).ToList();

            return Ok(allAlbums);
        }

        [HttpGet("GetAlbum/{albumId}")]
        public IActionResult GetAlbumById(int albumId)
        {
            var album = _albumService.GetAlbumById(albumId);
            if (album == null)
                return NotFound();
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                bool isAdmin = User.IsInRole("ADMIN");

                if (!_albumService.HasAccess(albumId, userId, isAdmin))
                    return Forbid();
                return Ok(album);
            }
        }
        [HttpGet("GetByName/{albumName}")]
        public IActionResult GetAlbumsByName(string albumName)
        {
            bool isAdmin = User.IsInRole("ADMIN");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var filteredAlbums = _albumService.GetAlbumsByName(albumName);

            if (filteredAlbums.Count == 0)
                return NotFound();
            else
            {
                if (isAdmin)
                    filteredAlbums = filteredAlbums;
                else
                    filteredAlbums = filteredAlbums.Where(p => p.Access == Data.AccessLevel.Public || p.UserId == userId).ToList();
            }

            return Ok(filteredAlbums);
        }
        [HttpGet("GetByAuthorId/{authorId}")]
        public IActionResult GetAlbumsByUserId(string authorId)
        {
            bool isAdmin = User.IsInRole("ADMIN");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var filteredAlbums = _albumService.GetAlbumsByAuthorId(authorId);

            if (filteredAlbums == null)
                return NotFound();
            else if (filteredAlbums.Count == 0)
                return NotFound();
            else
            {
                if (isAdmin)
                    filteredAlbums = filteredAlbums;
                else
                    filteredAlbums = filteredAlbums.Where(p => p.Access == Data.AccessLevel.Public || p.UserId == userId).ToList();

                return Ok(filteredAlbums);
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPost("AddPhotos")]
        public IActionResult AddPhotosByIds(int albumId, List<int> photoIds)
        {
            string currUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("ADMIN");

            bool statusChanged = _albumService.AddPhotoByIds(isAdmin, currUserId, albumId, photoIds);
            if (statusChanged)
                return Ok();
            else
                return BadRequest();
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPost("AddAlbum")]
        public IActionResult AddAlbumWithPhoto([FromForm] AlbumVM album)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("ADMIN");


            _albumService.AddAlbum(album, userId, isAdmin);
            return Ok(album);
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPut("UpdateAlbum/{albumId}")]
        public IActionResult UpdateAlbumById(int albumId, [FromForm] AlbumUpdateVM album)
        {
            string userId = _albumService.GetUserIdByAlbumId(albumId);
            bool isAdmin = User.IsInRole("ADMIN");
            string currUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!_albumService.AlbumExists(albumId))
            {
                return NotFound();
            }
            else
            {
                if (_albumService.HasPriveleges(albumId, currUserId, isAdmin))
                {
                    var photoUpd = _albumService.UpdateAlbumById(albumId, album, userId);
                    return Ok(photoUpd); ;
                }
                else
                    return Forbid();
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpPut("ChangeAccess/{albumId}")]
        public IActionResult ChangeAccess(int albumId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("ADMIN");

            if (_albumService.AlbumExists(albumId) == false)
                return NotFound();
            else if (_albumService.HasPriveleges(albumId, userId, isAdmin))
                return Ok(_albumService.ChangeAccessById(albumId));
            else
                return Forbid();
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpDelete("DeleteAlbum/{albumId}")]
        public IActionResult DeleteAlbumById(int albumId)
        {
            bool isAdmin = User.IsInRole("ADMIN");
            string currUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!_albumService.AlbumExists(albumId))
            {
                return NotFound();
            }
            else
            {
                if (_albumService.HasPriveleges(albumId, currUserId, isAdmin))
                {
                    _albumService.DeleteAlbumById(albumId);
                    return Ok();
                }
                else
                    return Forbid();
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        [HttpDelete("RemovePhotos")]
        public IActionResult RemovePhotosByIds(int albumId, List<int> photoIds)
        {
            string currUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isAdmin = User.IsInRole("ADMIN");

            bool statusChanged = _albumService.RemovePhotoByIds(isAdmin, currUserId, albumId, photoIds);
            if (statusChanged)
                return Ok();
            else
                return BadRequest();
        }           
    }
}

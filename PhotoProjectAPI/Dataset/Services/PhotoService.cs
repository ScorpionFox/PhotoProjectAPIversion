using PhotoProjectAPI.Dataset.Interfaces;
using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.DTO;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Claims;
using PhotoProjectAPI.Data;
//helper
namespace PhotoProjectAPI.Dataset.Services
{
    public class PhotoService : IPhotoService
    {
        private AppDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        public PhotoService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public string ChangeAccessUsingId(int photoId)
        {
            throw new NotImplementedException();
        }

        public void ChangeImageSize(string path, string pathThumbnails)
        {
            throw new NotImplementedException();
        }

        public void CretePhoto(PhotoViewmodel photo, string userId)
        {
            throw new NotImplementedException();
        }

        public void DeletePhotoUsingId(int photoId)
        {
            throw new NotImplementedException();
        }

        public bool DoesPhotoExist(int photoId)
        {
            throw new NotImplementedException();
        }

        public byte[] DownloadPhoto(string fileName)
        {
            throw new NotImplementedException();
        }

        public List<PhotoDTO> GetAllPhotos()
        {
            throw new NotImplementedException();
        }

        public string GetNextFileName(string fileName, string extension)
        {
            throw new NotImplementedException();
        }

        public string GetPathUsingFileName(string fileName)
        {
            throw new NotImplementedException();
        }

        public string GetPathUsingFileNameMiniatures(string fileName)
        {
            throw new NotImplementedException();
        }

        public List<PhotoDTO> GetPhotosUsingAuthorId(string authorId)
        {
            throw new NotImplementedException();
        }

        public List<PhotoDTO> GetPhotosUsingAuthorName(string authorName)
        {
            throw new NotImplementedException();
        }

        public List<PhotoDTO> GetPhotosUsingName(string photoName)
        {
            throw new NotImplementedException();
        }

        public Photo GetPhotoUsingFileName(string fileName)
        {
            throw new NotImplementedException();
        }

        public PhotoDTO GetPhotoUsingId(int photoId)
        {
            throw new NotImplementedException();
        }

        public Photo GetPhotoUsingIdPriv(int photoId)
        {
            throw new NotImplementedException();
        }

        public bool GetRatingsDetails(string userId, int photoId)
        {
            throw new NotImplementedException();
        }

        public string GetUserIdUsingPhotoId(int photoId)
        {
            throw new NotImplementedException();
        }

        public void GiveDislike(int photoId)
        {
            throw new NotImplementedException();
        }

        public void GiveLike(int photoId)
        {
            throw new NotImplementedException();
        }

        public bool HasPhotoAccess(int photoId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public bool HasPhotoPriveleges(int photoId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public PhotoDTO UpdatePhotoUsingId(int photoId, PhotoUpdateViewmodel photo, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UploadPhoto(IFormFile iFormFile, string path)
        {
            throw new NotImplementedException();
        }
    }
}

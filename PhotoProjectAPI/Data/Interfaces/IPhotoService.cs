using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Dto;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Data.Interfaces
{
    public interface IPhotoService
    {           
        public Photo GetPhotoByIdPriv(int photoId);
        public List<PhotoDto> GetAllPhotos();
        public PhotoDto GetPhotoById(int photoId);
        public string GetUserIdByPhotoId(int photoId);
        public List<PhotoDto> GetPhotosByAuthorId(string authorId);
        public List<PhotoDto> GetPhotosByName(string photoName);
        public string GetNextFileName(string fileName, string extension);
        public string GetPathByFileName(string fileName);

        public PhotoDto UpdatePhotoById(int photoId, PhotoUpdateVM photo, string userId);
        public string ChangeAccessById(int photoId);
        public Task UploadPhoto(IFormFile iFormFile, string path);      
        public byte[] DownloadPhoto(string fileName);
        void AddPhoto(PhotoVM photo, string userId);

        public bool HasAccess(int photoId, string userId, bool isAdmin);
        public bool HasPriveleges(int photoId, string userId, bool isAdmin);
        public bool PhotoExists(int photoId);

        public void DeletePhotoById(int photoId);

        public void AddUpvote(int photoId);
        public void AddDownvote(int photoId);
        public bool GetVoteDetails(string userId, int photoId);
    }
}

using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.DTO;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhotoProjectAPI.Dataset.Interfaces
{
    public interface IPhotoService
    {
        void CretePhoto(PhotoViewmodel photo, string userId);
        public List<PhotoDTO> GetAllPhotos();

        public Photo GetPhotoUsingIdPriv(int photoId);
        public PhotoDTO GetPhotoUsingId(int photoId);
        public Photo GetPhotoUsingFileName(string fileName);

        public List<PhotoDTO> GetPhotosUsingAuthorName(string authorName);
        public List<PhotoDTO> GetPhotosUsingAuthorId(string authorId);
        public List<PhotoDTO> GetPhotosUsingName(string photoName);
      

        public string GetUserIdUsingPhotoId(int photoId);
        public string GetPathUsingFileName(string fileName);
        public string GetPathUsingFileNameMiniatures(string fileName);
        public string GetNextFileName(string fileName, string extension);

        public bool DoesPhotoExist(int photoId);
        public void ChangeImageSize(string path, string pathThumbnails);
        public string ChangeAccessUsingId(int photoId);


        public PhotoDTO UpdatePhotoUsingId(int photoId, PhotoUpdateViewmodel photo, string userId);
        public Task UploadPhoto(IFormFile iFormFile, string path);
        public byte[] DownloadPhoto(string fileName);


        public bool HasPhotoAccess(int photoId, string userId, bool isAdmin);
        public bool HasPhotoPriveleges(int photoId, string userId, bool isAdmin);
        
        public void GiveLike(int photoId);
        public void GiveDislike(int photoId);
        public bool GetRatingsDetails(string userId, int photoId);

        public void DeletePhotoUsingId(int photoId);
    }
}

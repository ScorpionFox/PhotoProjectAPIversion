using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Dto;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhotoProjectAPI.Data.Interfaces
{
    public interface IAlbumService
    {      
        public List<AlbumDto> GetAllAlbums();
        public List<PhotoDto> GetAllPhotosFromAlbum(int albumId);
        public AlbumDto GetAlbumById(int albumId);
        public string GetUserIdByAlbumId(int albumId);
        public Album GetAlbumByIdPriv(int albumId);
        public List<AlbumDto> GetAlbumsByName(string albumName);
        public List<AlbumDto> GetAlbumsByAuthorId(string authorId);
        public AlbumPhoto GetAlbumPhotoByIds(int albumId, int photoId);

        public AlbumDto UpdateAlbumById(int albumId, [FromForm] AlbumUpdateVM album, string userId);
        public string ChangeAccessById(int albumId);
        public bool AddPhotoByIds(bool isAdmin, string currentUserId, int albumId, List<int> photoIds);
        public void AddAlbum(AlbumVM album, string userId, bool isAdmin);
        
        public bool HasAccess(int albumId, string userId, bool isAdmin);
        public bool HasPriveleges(int albumId, string userId, bool isAdmin);
        public bool HasPriveleges(int albumId, int destAlbumId, string userId, bool isAdmin);
        public bool HasPrivlegesToPhoto(int photoId, string userId, bool isAdmin);
        public bool AlbumExists(int albumId);

        public void DeleteAlbumById(int albumId);
        public bool RemovePhotoByIds(bool isAdmin, string currentUserId, int albumId, List<int> photoIds);
    }
}

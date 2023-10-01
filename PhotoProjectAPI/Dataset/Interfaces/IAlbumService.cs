using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.DTO;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhotoProjectAPI.Data.Interfaces
{
    public interface IAlbumService
    {
        public void CreateAlbum(AlbumViewmodel album, string userId, bool isAdmin);
        public List<AlbumDTO> GetAllAlbums();
        public List<PhotoDTO> GetAllPhotosFromAlbum(int albumId);

        public AlbumDTO GetAlbumUsingId(int albumId);
        public Album GetAlbumUsingIdPriv(int albumId);
        public List<AlbumDTO> GetAlbumsUsingName(string albumName);
        public List<AlbumDTO> GetAlbumsUsingAuthorName(string authorName);
        public List<AlbumDTO> GetAlbumsUsingAuthorId(string authorId);

        public string GetUserIdUsingAlbumId(int albumId);
        public PhotoAlbum GetAlbumPhotoUsingIds(int albumId, int photoId);

        public bool HasAlbumAccess(int albumId, string userId, bool isAdmin);
        public bool HasAlbumPrivileges(int albumId, string userId, bool isAdmin);
        public bool HasAlbumPrivilegesWithDestAlbumId(int albumId, int destAlbumId, string userId, bool isAdmin);
        public bool HasPhotoPrivileges(int photoId, string userId, bool isAdmin);

        public bool AddPhotoToAlbumUsingIds(bool isAdmin, string currentUserId, int albumId, List<int> photoIds);
        public bool MovePhotosToAlbum(bool isAdmin, string currentUserId, int currentAlbumId, int destinationAlbumId, List<int> photoIds);
        public bool RemovePhotoFromAlbumUsingId(bool isAdmin, string currentUserId, int albumId, List<int> photoIds);

        public bool DoesAlbumExist(int albumId);
        public string ChangeAlbumAccessUsingId(int albumId);
        public string ChangeAlbumAccessForAllUsingId(int albumId);
        public AlbumDTO UpdateAlbum(int albumId, [FromForm] AlbumUpdateViewmodel album, string userId);
       
        public void DeleteAlbumUsingId(int albumId);      
        public string DeleteAlbumAndPhotos(int albumId);
    }
}

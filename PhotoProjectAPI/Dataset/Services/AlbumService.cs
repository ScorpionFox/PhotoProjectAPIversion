using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.DTO;
using Microsoft.EntityFrameworkCore;
using PhotoProjectAPI.Data.Interfaces;

//trzeba helper w serwisach dorobić do albumu i do zdjęć żeby w jednym miejscu zapisywalo zdjęcia
namespace PhotoProjectAPI.Data.Services
{

    //zaimplementować
    public class AlbumService : IAlbumService
    {
        private AppDbContext _context;
        public AlbumService(AppDbContext context)
        {
            _context = context;
        }

        public bool AddPhotoToAlbumUsingIds(bool isAdmin, string currentUserId, int albumId, List<int> photoIds)
        {
            throw new NotImplementedException();
        }

        public string ChangeAlbumAccessForAllUsingId(int albumId)
        {
            throw new NotImplementedException();
        }

        public string ChangeAlbumAccessUsingId(int albumId)
        {
            throw new NotImplementedException();
        }

        public void CreateAlbum(AlbumViewmodel album, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public string DeleteAlbumAndPhotos(int albumId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAlbumUsingId(int albumId)
        {
            throw new NotImplementedException();
        }

        public bool DoesAlbumExist(int albumId)
        {
            throw new NotImplementedException();
        }

        public PhotoAlbum GetAlbumPhotoUsingIds(int albumId, int photoId)
        {
            throw new NotImplementedException();
        }

        public List<AlbumDTO> GetAlbumsUsingAuthorId(string authorId)
        {
            throw new NotImplementedException();
        }

        public List<AlbumDTO> GetAlbumsUsingAuthorName(string authorName)
        {
            throw new NotImplementedException();
        }

        public List<AlbumDTO> GetAlbumsUsingName(string albumName)
        {
            throw new NotImplementedException();
        }

        public AlbumDTO GetAlbumUsingId(int albumId)
        {
            throw new NotImplementedException();
        }

        public Album GetAlbumUsingIdPriv(int albumId)
        {
            throw new NotImplementedException();
        }

        public List<AlbumDTO> GetAllAlbums()
        {
            throw new NotImplementedException();
        }

        public List<PhotoDTO> GetAllPhotosFromAlbum(int albumId)
        {
            throw new NotImplementedException();
        }

        public string GetUserIdUsingAlbumId(int albumId)
        {
            throw new NotImplementedException();
        }

        public bool HasAlbumAccess(int albumId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public bool HasAlbumPrivileges(int albumId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public bool HasAlbumPrivilegesWithDestAlbumId(int albumId, int destAlbumId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public bool HasPhotoPrivileges(int photoId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public bool MovePhotosToAlbum(bool isAdmin, string currentUserId, int currentAlbumId, int destinationAlbumId, List<int> photoIds)
        {
            throw new NotImplementedException();
        }

        public bool RemovePhotoFromAlbumUsingId(bool isAdmin, string currentUserId, int albumId, List<int> photoIds)
        {
            throw new NotImplementedException();
        }

        public AlbumDTO UpdateAlbum(int albumId, [FromForm] AlbumUpdateViewmodel album, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

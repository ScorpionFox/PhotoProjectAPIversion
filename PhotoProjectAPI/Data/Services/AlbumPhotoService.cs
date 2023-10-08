using PhotoProjectAPI.Data.Interfaces;
using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Models;
using PhotoProjectAPI.Data;

namespace PhotoProjectAPI.Data.Services
{
    public class AlbumPhotoService : IAlbumPhotoService
    {
        private AppDbContext _context;
        public AlbumPhotoService(AppDbContext context)
        {
            _context = context;
        }

        public AlbumPhoto GetAlbumPhotoByIds(int albumId, int photoId)
        {
            var albumPhoto = _context.AlbumsPhotos.Where(p => p.PhotoId == photoId && p.AlbumId == albumId).FirstOrDefault();
            return albumPhoto;
        }
    }
}

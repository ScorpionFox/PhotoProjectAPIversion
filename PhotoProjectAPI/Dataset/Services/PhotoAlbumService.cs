using PhotoProjectAPI.Data;
using PhotoProjectAPI.Dataset.Interfaces;
using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Dataset.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        private AppDbContext _context;
        public PhotoAlbumService(AppDbContext context)
        {
            _context = context;
        }

        public PhotoAlbum GetPhotoAlbumUsingIds(int albumId, int photoId)
        {
            throw new NotImplementedException();
        }
    }
}

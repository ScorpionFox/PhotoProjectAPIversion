using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Data.Interfaces
{
    public interface IAlbumPhotoService
    {
        AlbumPhoto GetAlbumPhotoByIds(int albumId, int photoId);
    }
}

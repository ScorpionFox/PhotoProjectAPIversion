using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Dataset.Interfaces
{
    public interface IPhotoAlbumService
    {
        PhotoAlbum GetPhotoAlbumUsingIds(int albumId, int photoId);
    }
}

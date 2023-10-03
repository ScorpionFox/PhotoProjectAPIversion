using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Repository.Interface
{
    public interface IAlbumRepostiory
    {
        Task<List<Album>> GetAlbum(int? albumId = null, string? accessNumber = null);
    }
}

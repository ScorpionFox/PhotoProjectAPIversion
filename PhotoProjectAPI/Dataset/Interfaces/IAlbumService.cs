using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.DTO;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhotoProjectAPI.Data.Interfaces
{
    public interface IAlbumService
    {
        Task<List<AlbumDTO>> GetAlbumAsync(int? albumid = null, string? accessNumber = null);
        Task<int> AddAlbumAsync(AlbumViewmodel albumViewmodel);
    }
}

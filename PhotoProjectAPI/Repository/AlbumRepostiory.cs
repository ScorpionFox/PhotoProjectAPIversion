using Microsoft.EntityFrameworkCore;
using PhotoProjectAPI.Data;
using PhotoProjectAPI.Dataset;
using PhotoProjectAPI.Models;
using PhotoProjectAPI.Repository.Interface;

namespace PhotoProjectAPI.Repository
{
    public class AlbumRepostiory : IAlbumRepostiory
    {
       private readonly AppDbContext _context;
       private readonly string _sql;

        public AlbumRepostiory(AppDbContext context)
        {
            _context = context;
            _sql = @"Select *
                    from Albums";
        }
        public async Task<List<Album>> GetAlbum(int? albumId = null, string? accessNumber = null)
        {
            var query = _context.Album.FromSqlRaw(_sql).AsQueryable();
            if(albumId != null)
                query = query.Where(x => x.Id == albumId);
            if (accessNumber != null)
                switch (accessNumber)
                {
                    case "Private":
                        query = query.Where(x => x.Access == Accessibility.Private);
                        break;
                    case "Public":
                        query = query.Where(x => x.Access == Accessibility.Public);
                        break;
                    default:
                        break;
                }
            return await query.ToListAsync();
        }

    }
}

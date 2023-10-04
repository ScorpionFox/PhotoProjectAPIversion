using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.DTO;
using Microsoft.EntityFrameworkCore;
using PhotoProjectAPI.Data.Interfaces;
using PhotoProjectAPI.Data;
using PhotoProjectAPI.Repository.Interface;
using AutoMapper;
using PhotoProjectAPI.Repository;

namespace PhotoProjectAPI.Data.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepostiory _albumRepository;  // Poprawiłem literówkę tutaj
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;  // Zmieniłem typ tutaj

        public AlbumService(IAlbumRepostiory albumRepository, IMapper mapper, AppDbContext appContext)  // Zmieniłem typ tutaj
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
            _context = appContext;
        }

        public async Task<List<AlbumDTO>> GetAlbumAsync(int? albumid = null, string? accessNumber = null)
        {
            var album = await _albumRepository.GetAlbum(albumid, accessNumber).ConfigureAwait(false);
            return _mapper.Map<List<AlbumDTO>>(album);
        }

        public async Task<int> AddAlbumAsync(AlbumViewmodel albumViewmodel)
        {

            var albumEntity = new Album
            {
                Name = albumViewmodel.Name,
                Access = albumViewmodel.Access
            };

            _context.Album.Add(albumEntity); 

            await _context.SaveChangesAsync();
            return albumEntity.Id;
        }
    }
}


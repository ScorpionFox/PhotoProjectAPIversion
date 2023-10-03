using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using PhotoProjectAPI.DTO;
using Microsoft.EntityFrameworkCore;
using PhotoProjectAPI.Data.Interfaces;
using PhotoProjectAPI.Data;
using PhotoProjectAPI.Repository.Interface;
using AutoMapper;

namespace PhotoProjectAPI.Data.Services
{

    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepostiory _albumRepository;
        private readonly IMapper _mapper;

        public AlbumService(IAlbumRepostiory albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        public async Task<List<AlbumDTO>> GetAlbumAsync(int? albumid = null, string? accessNumber = null)
        {
            var album = await _albumRepository.GetAlbum(albumid,accessNumber).ConfigureAwait(false);
            return _mapper.Map<List<AlbumDTO>>(album);
        }
    }
}

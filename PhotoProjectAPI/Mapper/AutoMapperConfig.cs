using AutoMapper;
using PhotoProjectAPI.DTO;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Album, AlbumDTO>();
            });

            return config.CreateMapper();
        }
    }
}


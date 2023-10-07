using PhotoProjectAPI.Data;
using PhotoProjectAPI.Models;
using PhotoProjectAPI.Dto;

namespace PhotoProjectAPI.Dto
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccessLevel Access { get; set; }
        public string? User { get; set; }
        public string UserId { get; set; }
        public List<PhotoDto> Photos { get; set; }

    }
}

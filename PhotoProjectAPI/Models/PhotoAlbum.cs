using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Models
{
    public class PhotoAlbum
    {
        [Key]
        public int Id { get; set; }
        public int? AlbumId { get; set; }
        public Album? Album { get; set; }
        public int? PhotoId { get; set; }
        public Photo? Photo { get; set; }
        
    }
}

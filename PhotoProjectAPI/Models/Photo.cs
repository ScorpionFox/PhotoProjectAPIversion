using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace PhotoProjectAPI.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Tags { get; set; }
        public string Camera { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }

        public List<PhotoAlbum>? AlbumsPhotos { get; set; }

        public List<Comment>? Comments { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

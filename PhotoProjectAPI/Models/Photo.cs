using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PhotoProjectAPI.Data;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Tags { get; set; }
        public string Camera { get; set; }
        public AccessLevel Access { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public List<Rate>? Rates { get; set; }
        public List<AlbumPhoto>? AlbumsPhotos { get; set; }
        public List<Comment>? Comments { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

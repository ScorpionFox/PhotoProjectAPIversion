using PhotoProjectAPI.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public AccessLevel Access { get; set; }
        // relationship photos >--< albums
        public List<AlbumPhoto>? AlbumsPhotos { get; set; }
        // relationship user ---< photos
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PhotoProjectAPI.Dataset;

namespace PhotoProjectAPI.Models
{
    public class Album
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PhotoAlbum>? PhotoAlbums { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Accessibility Access { get; set; }
    }
}

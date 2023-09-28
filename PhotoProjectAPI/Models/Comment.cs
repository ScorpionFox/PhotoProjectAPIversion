using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Comments { get; set; }

        public int? PhotoId { get; set; }
        [ForeignKey("PhotoId")]
        public Photo? Photo { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

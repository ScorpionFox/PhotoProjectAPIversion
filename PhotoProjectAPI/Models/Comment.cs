using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Comments { get; set; }


        // relationship photo <--> comment
        public int? PhotoId { get; set; }
        [ForeignKey("PhotoId")]
        public Photo? Photo { get; set; }

        //relationship user<--< comments
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}

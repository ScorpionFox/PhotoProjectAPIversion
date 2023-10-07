using PhotoProjectAPI.Data;
using PhotoProjectAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProjectAPI.Dto
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Camera { get; set; }
        public AccessLevel Access { get; set; }
        public string ImageName { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public string? User { get; set; }
        public string UserId { get; set; }
    }
}

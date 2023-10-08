using Microsoft.AspNetCore.Identity;
using PhotoProjectAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Models
{
    public class User : IdentityUser
    {
        // relationships
        public List<Photo>? Photos { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}

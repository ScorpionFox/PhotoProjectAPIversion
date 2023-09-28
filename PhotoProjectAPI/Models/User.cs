using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace PhotoProjectAPI.Models
{
    public class User : IdentityUser
    {
        public List<Comment>? Comments { get; set; }
        public List<Photo>? Photos { get; set; }
        
    }
}

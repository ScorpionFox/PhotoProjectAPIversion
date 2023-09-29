using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProjectAPI.Models
{
    public class TokenRefreshment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string IdJWT { get; set; }
        public bool IsExpired { get; set; }


        public DateTime AddingDate { get; set; }
        public DateTime ExpiringDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}

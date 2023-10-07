using PhotoProjectAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Data.ViewModels
{

    public class AlbumVM
    {
        public string Name { get; set; }
        public AccessLevel Access { get; set; }
        public List<int>? PhotoId { get; set; }

    }
    public class AlbumUpdateVM
    {
        public string Name { get; set; }
        public AccessLevel Access { get; set; }

    }
}


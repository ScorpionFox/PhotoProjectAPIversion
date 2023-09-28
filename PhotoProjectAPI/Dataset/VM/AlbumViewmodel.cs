using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Dataset.VM
{

    public class AlbumViewmodel
    {
        public string Name { get; set; }       
        public List<int>? PhotoId { get; set; }

        public Accessibility Access { get; set; }
    }
    public class AlbumUpdateViewmodel
    {
        public string Name { get; set; }

        public Accessibility Access { get; set; }
        
    }
}


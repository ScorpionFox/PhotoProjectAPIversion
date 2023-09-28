using PhotoProjectAPI.Dataset;
using PhotoProjectAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoProjectAPI.Dataset.VM
{
    public class PhotoViewmodel
    {
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Camera { get; set; }
        public IFormFile ImageFile { get; set; }

        public Accessibility Access { get; set; }      
    }

    public class PhotoUpdateVM
    {
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Camera { get; set; }

        public Accessibility Access { get; set; }
    }

}

﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Models
{
    public class Album
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PhotoAlbum>? AlbumsPhotos { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

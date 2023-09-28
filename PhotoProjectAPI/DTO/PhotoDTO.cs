﻿using PhotoProjectAPI.Dataset;

namespace PhotoProjectAPI.DTO
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string? User { get; set; }
        public string UserId { get; set; }
        public string Tags { get; set; }
        public string Camera { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public Accessibility Access { get; set; }
    }
}
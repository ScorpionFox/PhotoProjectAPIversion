using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace PhotoProjectAPI.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<User> Users { get; set; }


        /* dokonczyc a potem migracja, jak projekt bedzie dzialal to na koncu AppDbSeeder jakis trzeba zrobic */

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoAlbum>().HasKey(ap => new
            {
                ap.Id,
            });
            modelBuilder.Entity<PhotoAlbum>()
                .HasOne(a => a.Photo)
                .WithMany(ap => ap.PhotoAlbums)
                .HasForeignKey(a => a.PhotoId);
            modelBuilder.Entity<PhotoAlbum>()
                .HasOne(a => a.Album)
                .WithMany(ap => ap.PhotoAlbums)
                .HasForeignKey(a => a.AlbumId);
            modelBuilder.Entity<Photo>()
                .HasMany<Comment>(c => c.Comments)
                .WithOne(s => s.Photo)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);


        }
    }
}
      
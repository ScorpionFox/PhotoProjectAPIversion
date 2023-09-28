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
        public DbSet<Album> Albums { get; set; }
    /* dokonczyc a potem migracja, jak projekt bedzie dzialal to na koncu AppDbSeeder jakis trzeba zrobic */

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
   
        }
    }
}

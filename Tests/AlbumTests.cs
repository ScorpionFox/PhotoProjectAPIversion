using PhotoProjectAPI.Data.Services;
using PhotoProjectAPI.Data;
using PhotoProjectAPI.Dto;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class AlbumServiceTests
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "AppDbTest")
            .Options;

        AppDbContext context;
        PhotoService photoService;
        AlbumService albumService;
        IWebHostEnvironment webHostEnvironment;

        [OneTimeSetUp] 
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            photoService = new PhotoService(context, webHostEnvironment);
            albumService = new AlbumService(context);
            var albumy = context.Albums;
            var zdjecia = context.Photos;
        }

        [Test, Order(1)]
        public void GetAlbum()
        {
            var result = albumService.GetAlbumByIdPriv(3);
            Assert.That(result.Id, Is.EqualTo(3));
            Assert.AreEqual(typeof(Album), result.GetType());
        }
        [Test, Order(2)]
        public void GetAlbum_WrongData()
        {
            var result = albumService.GetAlbumByIdPriv(69);
            Assert.That(result, Is.Null);
        }
        [Test, Order(3)]
        public void CheckIfAlbumExists()
        {
            var result = albumService.AlbumExists(4);
            Assert.That(result, Is.True);
        }
        [Test, Order(4)]
        public void CheckIfAlbumExists_WrongData()
        {
            var result = albumService.AlbumExists(89);
            Assert.That(result, Is.False);
        }
        [Test, Order(5)]
        public void ChangeAccess()
        {
            var result = albumService.GetAlbumByIdPriv(3);
            Assert.That(result.Access, Is.EqualTo(AccessLevel.Public));
            albumService.ChangeAccessById(3);
            Assert.That(result.Access, Is.EqualTo(AccessLevel.Private));
        }

        [Test, Order(6)]
        public void GetUserIdByPhotoAndAlbumId_WrongData()
        {
            var result = albumService.ChangeAccessById(11);
            Assert.That(result, Is.Null);
        }

        [Test, Order(7)]
        public void GetAlbumPhoto()
        {
            var result = albumService.GetAlbumPhotoByIds(4, 8);
            Assert.That(result.Id, Is.EqualTo(3));
        }  

        [Test, Order(8)]
        public void GetUserIdFromAlbumId()
        {
            var result = albumService.GetUserIdByAlbumId(3);
            Assert.That(result, Is.EqualTo("5678"));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var photos = new List<Photo>()
    {
        new Photo()
        {
            Id = 7,
            Access = AccessLevel.Public,
            Camera = "Nikon D750",
            ImageName = "beach.jpg",
            Name = "Sunny Beach",
            Tags = "vacation",
            UserId = "5678",
        },
        new Photo()
        {
            Id = 8,
            Access = AccessLevel.Private,
            Camera = "Canon EOS 6D",
            ImageName = "forest.jpg",
            Name = "Enchanted Forest",
            Tags = "nature",
            UserId = "5678",
        },
    };
            context.Photos.AddRange(photos);

            var photosDto = new List<PhotoDto>()
    {
        new PhotoDto()
        {
            Id = 7,
            Access = AccessLevel.Public,
            Camera = "Nikon D750",
            ImageName = "beach.jpg",
            Name = "Sunny Beach",
            Tags = "vacation",
            UserId = "5678",
        },
        new PhotoDto()
        {
            Id = 8,
            Access = AccessLevel.Private,
            Camera = "Canon EOS 6D",
            ImageName = "forest.jpg",
            Name = "Enchanted Forest",
            Tags = "nature",
            UserId = "5678",
        },
    };

            var albums = new List<Album>()
    {
        new Album()
        {
            Id = 3,
            Access = AccessLevel.Public,
            Name = "Vacation Memories",
            UserId = "5678"
        },
        new Album()
        {
            Id = 4,
            Access = AccessLevel.Private,
            Name = "Family Reunion",
            UserId = "5678"
        },
    };
            context.Albums.AddRange(albums);

            var ap = new List<AlbumPhoto>()
    {
        new AlbumPhoto()
        {
            AlbumId = 3,
            PhotoId = 8,
        },
        new AlbumPhoto()
        {
            AlbumId = 4,
            PhotoId = 7,
        },
        new AlbumPhoto()
        {
            AlbumId = 4,
            PhotoId = 8,
        }
    };
            context.AlbumsPhotos.AddRange(ap);

            var comments = new List<Comment>()
    {
        new Comment()
        {
            Comments = "Fantastic shot!",
            Id = 2,
            PhotoId= 7,
            UserId = "5678",
        }
    };
            context.Comments.AddRange(comments);

            context.SaveChanges();
        }

    }
}


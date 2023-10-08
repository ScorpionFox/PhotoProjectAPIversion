using PhotoProjectAPI.Data;
using PhotoProjectAPI.Data.Services;
using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Dto;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class PhotoServiceTests
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
        public void GetPhoto()
        {
            var result = photoService.GetPhotoByIdPriv(10);
            Assert.That(result.Id, Is.EqualTo(10));
            Assert.That(result.Name, Is.EqualTo("Beautiful Sunset"));
        }

        [Test, Order(2)]
        public void GetPhotoWrongData()
        {
            var result = photoService.GetPhotoByIdPriv(94);
            Assert.That(result, Is.Null);
        }
        [Test, Order(3)]
        public void CheckIfPhotoExists()
        {
            var result = photoService.PhotoExists(10);
            Assert.That(result, Is.True);
        }
        [Test, Order(4)]
        public void CheckIfPhotoExistsWrongData()
        {
            var result = photoService.PhotoExists(55);
            Assert.That(result, Is.False);
        }

        [Test, Order(5)]
        public void DeletePhotoThatExists()
        {
            var result = photoService.PhotoExists(10);
            Assert.That(result, Is.True);
            photoService.DeletePhotoById(10);
            result = photoService.PhotoExists(10);
            Assert.That(result, Is.False);
        }

        [Test, Order(6)]
        public void ChangeAccess()
        {
            var result = photoService.GetPhotoByIdPriv(11);
            Assert.That(result.Access, Is.EqualTo(AccessLevel.Private));
            photoService.ChangeAccessById(11);
            Assert.That(result.Access, Is.EqualTo(AccessLevel.Public));
        }
        [Test, Order(7)]
        public void GetUserIdByPhotoId()
        {
            var result = photoService.GetUserIdByPhotoId(11);
            Assert.That(result, Is.EqualTo("5678"));
        }

        [Test, Order(8)]
        public void Likes()
        {
            photoService.AddUpvote(11);
            photoService.AddDownvote(11);
            var result = photoService.GetPhotoByIdPriv(11);
            Assert.That(result.UpVotes, Is.EqualTo(1));
            Assert.That(result.DownVotes, Is.EqualTo(1));
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
            Id = 10,
            Access = AccessLevel.Public,
            Camera = "Canon EOS 5D Mark IV",
            ImageName = "sunset.jpg",
            Name = "Beautiful Sunset",
            Tags = "scenic",
            UserId = "5678",
        },
        new Photo()
        {
            Id = 11,
            Access = AccessLevel.Private,
            Camera = "Nikon D850",
            ImageName = "mountains.jpg",
            Name = "Mountain Adventure",
            Tags = "nature",
            UserId = "5678",
        },
    };
            context.Photos.AddRange(photos);

            var photosDto = new List<PhotoDto>()
    {
        new PhotoDto()
        {
            Id = 10,
            Access = AccessLevel.Public,
            Camera = "Canon EOS 5D Mark IV",
            ImageName = "sunset.jpg",
            Name = "Beautiful Sunset",
            Tags = "scenic",
            UserId = "5678",
        },
        new PhotoDto()
        {
            Id = 11,
            Access = AccessLevel.Private,
            Camera = "Nikon D850",
            ImageName = "mountains.jpg",
            Name = "Mountain Adventure",
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
            PhotoId = 11,
        },
        new AlbumPhoto()
        {
            AlbumId = 4,
            PhotoId = 10,
        },
        new AlbumPhoto()
        {
            AlbumId = 4,
            PhotoId = 11,
        }
    };
            context.AlbumsPhotos.AddRange(ap);

            var comments = new List<Comment>()
    {
        new Comment()
        {
            Comments = "Amazing shot!",
            Id = 2,
            PhotoId= 10,
            UserId = "5678",
        }
    };
            context.Comments.AddRange(comments);

            context.SaveChanges();
        }

    }
}
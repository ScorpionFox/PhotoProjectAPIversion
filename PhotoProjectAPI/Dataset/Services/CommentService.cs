using PhotoProjectAPI.Dataset.Interfaces;
using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.DTO;
using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoProjectAPI.Data;

namespace PhotoProjectAPI.Dataset.Services
{
    public class CommentService : ICommentService
    {
        private AppDbContext _context;
        private PhotoService _photoService;
        public CommentService(AppDbContext context, PhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public void CreateComment(CommentsViewmodel comment, string userId, int photoId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommentUsingId(int commentId)
        {
            throw new NotImplementedException();
        }

        public bool DoesCommentExist(int commentId)
        {
            throw new NotImplementedException();
        }

        public List<CommentDTO> GetAllCommentsUsingPhoto(int photoId)
        {
            throw new NotImplementedException();
        }

        public CommentDTO GetCommentUsingId(int commentId)
        {
            throw new NotImplementedException();
        }

        public Comment GetCommentUsingIdPriv(int commentId)
        {
            throw new NotImplementedException();
        }

        public bool HasCommentPriveleges(int commentId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthor(int photoId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }
    }
}

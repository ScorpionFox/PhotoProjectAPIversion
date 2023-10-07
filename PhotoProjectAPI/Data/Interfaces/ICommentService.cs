using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Dto;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Data.Interfaces
{
    public interface ICommentService
    {
        public void AddComment(CommentVM comment, string userId, int photoId);
        public bool IsAuthor(int photoId, string userId, bool isAdmin);
        public List<CommentDto> GetAllCommentsByPhoto(int photoId);
        public bool HasPriveleges(int commentId, string userId, bool isAdmin);
        public CommentDto GetCommentById(int commentId);
        public Comment GetCommentByIdPriv(int commentId);
        public bool CommentExists(int commentId);
        public void DeleteCommentById(int commentId);

    }
}

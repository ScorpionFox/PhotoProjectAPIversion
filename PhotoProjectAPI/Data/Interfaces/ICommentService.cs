using PhotoProjectAPI.Data.ViewModels;
using PhotoProjectAPI.Dto;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Data.Interfaces
{
    public interface ICommentService
    {           
        public List<CommentDto> GetAllCommentsByPhoto(int photoId);       
        public CommentDto GetCommentById(int commentId);
        public Comment GetCommentByIdPriv(int commentId);

        public void AddComment(CommentVM comment, string userId, int photoId);

        public bool CommentExists(int commentId);
        public bool HasPriveleges(int commentId, string userId, bool isAdmin);
        public bool IsAuthor(int photoId, string userId, bool isAdmin);

        public void DeleteCommentById(int commentId);

    }
}

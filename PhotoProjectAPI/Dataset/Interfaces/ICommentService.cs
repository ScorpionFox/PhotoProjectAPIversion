using PhotoProjectAPI.Dataset.VM;
using PhotoProjectAPI.DTO;
using PhotoProjectAPI.Models;

namespace PhotoProjectAPI.Dataset.Interfaces
{
    public interface ICommentService
    {
        public void CreateComment(CommentsViewmodel comment, string userId, int photoId);
        public List<CommentDTO> GetAllCommentsUsingPhoto(int photoId);

        public CommentDTO GetCommentUsingId(int commentId);
        public Comment GetCommentUsingIdPriv(int commentId);

        public bool IsAuthor(int photoId, string userId, bool isAdmin);
        public bool HasCommentPriveleges(int commentId, string userId, bool isAdmin);
        public bool DoesCommentExist(int commentId);

        public void DeleteCommentUsingId(int commentId);

    }
}

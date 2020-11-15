namespace FastServices.Services.Comments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface ICommentsService
    {
        public IEnumerable<Comment> GetAllComments();

        public Task AddCommentAsync(Comment comment);

        public void HardDeleteComment(Comment comment);

        public void DeleteComment(Comment comment);

        public Comment GetCommentById(int id);
    }
}

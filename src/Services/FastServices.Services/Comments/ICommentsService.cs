namespace FastServices.Services.Comments
{
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface ICommentsService
    {
        public Task AddComment(Comment comment);

        public Task DeleteComment(Comment comment);

        public Comment GetCommentById(int id);
    }
}

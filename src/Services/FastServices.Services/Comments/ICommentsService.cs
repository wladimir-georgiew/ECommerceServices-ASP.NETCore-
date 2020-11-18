namespace FastServices.Services.Comments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface ICommentsService
    {
        public IQueryable<Comment> GetAll();

        public Task AddCommentAsync(Comment comment);

        public Task DeleteCommentAsync(Comment comment);

        public Comment GetById(int id);
    }
}

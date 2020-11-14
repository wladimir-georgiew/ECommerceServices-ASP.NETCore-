namespace FastServices.Services.Comments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext db;

        public CommentsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddComment(Comment comment)
        {
            await this.db.AddAsync(comment);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteComment(Comment comment)
        {
            this.db.Remove(comment);
            await this.db.SaveChangesAsync();
        }

        public Comment GetCommentById(int id)
        {
            return this.db.Comments.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}

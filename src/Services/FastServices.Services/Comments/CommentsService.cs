namespace FastServices.Services.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext db;
        private readonly IRepository<Comment> repository;

        public CommentsService(ApplicationDbContext db, IRepository<Comment> repository)
        {
            this.db = db;
            this.repository = repository;
        }

        public IEnumerable<Comment> GetAllComments() => this.repository.All();

        public async Task AddCommentAsync(Comment comment)
        {
            await this.repository.AddAsync(comment);
            await this.db.SaveChangesAsync();
        }

        public void HardDeleteComment(Comment comment)
        {
            this.repository.Delete(comment);
            this.db.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            this.repository.Delete(comment);
            this.db.SaveChanges();
        }

        public Comment GetCommentById(int id) => this.GetAllComments().FirstOrDefault(x => x.Id == id);
    }
}

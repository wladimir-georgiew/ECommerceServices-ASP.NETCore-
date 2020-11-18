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
        private readonly IRepository<Comment> repository;

        public CommentsService(IRepository<Comment> repository)
        {
            this.repository = repository;
        }

        public IQueryable<Comment> GetAll() => this.repository.All();

        public async Task AddCommentAsync(Comment comment)
        {
            await this.repository.AddAsync(comment);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            this.repository.Delete(comment);
            await this.repository.SaveChangesAsync();
        }

        public Comment GetById(int id) => this.GetAll().FirstOrDefault(x => x.Id == id);
    }
}

namespace FastServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Data.Repositories;
    using FastServices.Services.Comments;
    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class CommentsSerivceTests
    {
        private List<Comment> list;
        private Mock<IRepository<Comment>> repository;

        public CommentsSerivceTests()
        {
            this.list = new List<Comment>();
            this.repository = new Mock<IRepository<Comment>>();
        }

        [Fact]
        public void GetAllCountShouldReturnCorrectNumber()
        {
            this.repository.Setup(r => r.All()).Returns(new List<Comment>
                                                        {
                                                            new Comment(),
                                                            new Comment(),
                                                            new Comment(),
                                                        }.AsQueryable());

            var service = new CommentsService(this.repository.Object);

            Assert.Equal(3, service.GetAll().Count());

            this.repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task AddShouldAddCommentCorrectly()
        {
            this.repository.Setup(r => r.All()).Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Comment>()))
                .Callback((Comment comment) => this.list.Add(comment));

            var service = new CommentsService(this.repository.Object);

            await service.AddCommentAsync(new Comment
            {
                Id = 25,
                CommentContent = "testc",
            });

            var comment = this.list.FirstOrDefault(x => x.Id == 25);

            Assert.NotNull(comment);
        }

        [Fact]
        public void GetByIdShouldReturnNullObjectIfIdDoesntExist()
        {
            this.repository.Setup(r => r.All()).Returns(this.list.AsQueryable());

            // ACT
            var service = new CommentsService(this.repository.Object);

            var comment = service.GetById(2000);

            Assert.Null(comment);
        }

        [Fact]
        public async Task GetByIdShouldReturnActualObjectIfIdExists()
        {
            this.repository.Setup(r => r.All()).Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Comment>()))
                .Callback((Comment comment) => this.list.Add(comment));

            var service = new CommentsService(this.repository.Object);

            await service.AddCommentAsync(new Comment
            {
                Id = 25,
                CommentContent = "testc",
            });

            // ACT
            var comment = service.GetById(25);

            Assert.NotNull(comment);
        }

        [Fact]
        public async Task DeleteShouldDeleteTheCommentIfItExists()
        {
            this.repository.Setup(r => r.All()).Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Comment>()))
                .Callback((Comment comment) => this.list.Add(comment));
            this.repository.Setup(r => r.Delete(It.IsAny<Comment>()))
                .Callback((Comment comment) => this.list.Remove(comment));

            var service = new CommentsService(this.repository.Object);

            await service.AddCommentAsync(new Comment
            {
                Id = 25,
                CommentContent = "testc",
            });

            var comment = this.list.FirstOrDefault(x => x.Id == 25);

            // ACT
            await service.DeleteCommentAsync(comment);
            var notExistingComment = service.GetById(25);

            Assert.Null(notExistingComment);
        }
    }
}

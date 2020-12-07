namespace FastServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Data.Repositories;
    using FastServices.Services.Comments;
    using FastServices.Services.Departments;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class DepartmentsServiceTests
    {
        private List<Department> list;
        private Mock<IDeletableEntityRepository<Department>> repository;
        private IWebHostEnvironment dep1;

        public DepartmentsServiceTests()
        {
            this.list = new List<Department>();
            this.repository = new Mock<IDeletableEntityRepository<Department>>();
        }

        [Fact]
        public async Task AddDepartmentAsyncShouldAddCorrectly()
        {
            this.repository.Setup(r => r.All()).Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .Callback((Department department) => this.list.Add(department));

            var service = new DepartmentsService(
                this.repository.Object,
                new ImageServices(this.dep1));

            await service.AddCommentAsync(new Comment
            {
                Id = 25,
                CommentContent = "testc",
            });

            var comment = this.list.FirstOrDefault(x => x.Id == 25);

            Assert.NotNull(comment);
        }
    }
}

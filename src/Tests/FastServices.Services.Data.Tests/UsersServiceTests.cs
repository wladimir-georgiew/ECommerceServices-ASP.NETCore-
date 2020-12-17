namespace FastServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Services.Comments;
    using FastServices.Services.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Xunit;

    public class UsersServiceTests
    {
        private Mock<UserManager<ApplicationUser>> userManager;
        private Mock<RoleManager<ApplicationRole>> roleManager;

        private List<ApplicationUser> list;
        private List<Comment> commentsList;
        private Mock<IDeletableEntityRepository<ApplicationUser>> repository;
        private Mock<IRepository<Comment>> commentsRepository;

        public UsersServiceTests()
        {
            this.list = new List<ApplicationUser>();
            this.commentsList = new List<Comment>();
            this.repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            this.commentsRepository = new Mock<IRepository<Comment>>();
            this.userManager = this.GetMockUserManager();
            this.roleManager = this.GetMockRoleManager();
        }

        [Fact]
        public void GetAllShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
               .Callback((ApplicationUser user) => this.list.Add(user));

            var commentsService = new CommentsService(this.commentsRepository.Object);

            var service = new UsersService(
                this.repository.Object,
                commentsService,
                this.roleManager.Object,
                this.userManager.Object);

            this.list.Add(new ApplicationUser());

            var users = service.GetAll();

            Assert.NotNull(users);
            Assert.Single(users);
        }

        [Fact]
        public void GetUserLatestCommentShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.commentsRepository.Setup(r => r.All())
             .Returns(this.commentsList.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
               .Callback((ApplicationUser user) => this.list.Add(user));

            var commentsService = new CommentsService(this.commentsRepository.Object);

            var service = new UsersService(
                this.repository.Object,
                commentsService,
                this.roleManager.Object,
                this.userManager.Object);

            this.commentsList.Add(new Comment { Id = 1, ApplicationUserId = "userid", CreatedOn = DateTime.UtcNow });
            this.commentsList.Add(new Comment { Id = 2, ApplicationUserId = "userid", CreatedOn = DateTime.UtcNow.AddDays(2) });
            this.commentsList.Add(new Comment { Id = 3, ApplicationUserId = "userid", CreatedOn = DateTime.UtcNow });

            var latestComment = service.GetUserLatestComment("userid");

            Assert.NotNull(latestComment);
            Assert.Equal(2, latestComment.Id);
        }

        [Fact]
        public void IsUserAllowedToCommentShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.commentsRepository.Setup(r => r.All())
             .Returns(this.commentsList.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
               .Callback((ApplicationUser user) => this.list.Add(user));

            var commentsService = new CommentsService(this.commentsRepository.Object);

            var service = new UsersService(
                this.repository.Object,
                commentsService,
                this.roleManager.Object,
                this.userManager.Object);

            this.list.Add(new ApplicationUser { Id = "userid" });
            this.list.Add(new ApplicationUser { Id = "userid2" });

            this.commentsList.Add(new Comment { Id = 1, ApplicationUserId = "userid", CreatedOn = DateTime.UtcNow });
            this.commentsList.Add(new Comment { Id = 1, ApplicationUserId = "userid2", CreatedOn = DateTime.UtcNow.AddDays(-1) });

            var falseUser = service.IsUserAllowedToComment("userid");
            var trueUser = service.IsUserAllowedToComment("userid2");

            Assert.False(falseUser);
            Assert.True(trueUser);
        }

        [Fact]
        public void IsUserAllowedToSubmitOrderShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.commentsRepository.Setup(r => r.All())
             .Returns(this.commentsList.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<ApplicationUser>()))
               .Callback((ApplicationUser user) => this.list.Add(user));

            var commentsService = new CommentsService(this.commentsRepository.Object);

            var service = new UsersService(
                this.repository.Object,
                commentsService,
                this.roleManager.Object,
                this.userManager.Object);

            this.list.Add(new ApplicationUser
            {
                Id = "falseUserId",
                Orders = new List<Order>
                {
                    new Order { Status = FastServices.Data.Models.Enumerators.OrderStatus.Active },
                },
            });

            this.list.Add(new ApplicationUser
            {
                Id = "trueUserId",
                Orders = new List<Order>
                {
                    new Order { Status = FastServices.Data.Models.Enumerators.OrderStatus.Completed },
                },
            });

            var isFalseUserAllowedToSubmitOrder = service.IsUserAllowedToSubmitOrder("falseUserId");
            var isTrueUserAllowedToSubmitOrder = service.IsUserAllowedToSubmitOrder("trueUserId");

            Assert.False(isFalseUserAllowedToSubmitOrder);
            Assert.True(isTrueUserAllowedToSubmitOrder);
        }

        private Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        private Mock<RoleManager<ApplicationRole>> GetMockRoleManager()
        {
            var roleStore = new Mock<IRoleStore<ApplicationRole>>();
            return new Mock<RoleManager<ApplicationRole>>(
                         roleStore.Object, null, null, null, null);
        }
    }
}

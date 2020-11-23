namespace FastServices.Services.Users
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Services.Comments;
    using FastServices.Services.Orders;
    using FastServices.Web.ViewModels.Orders;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> repository;
        private readonly ICommentsService commentsService;
        private readonly IOrdersService ordersService;
        private readonly ApplicationDbContext db;

        public UsersService(IDeletableEntityRepository<ApplicationUser> repository, ICommentsService commentsService, IOrdersService ordersService, ApplicationDbContext db)
        {
            this.repository = repository;
            this.commentsService = commentsService;
            this.ordersService = ordersService;
            this.db = db;
        }

        public bool IsUserAllowedToComment(string userId)
        {
            var userLatestComment = this.GetUserLatestComment(userId);

            if (userLatestComment != null)
            {
                var dateDiff = DateTime.UtcNow.Subtract(userLatestComment.CreatedOn);

                if (dateDiff.Days < 1)
                {
                    return false;
                }
            }

            return true;
        }

        public Comment GetUserLatestComment(string userId) => this.commentsService.GetAll().Where(x => x.ApplicationUserId == userId).OrderByDescending(x => x.CreatedOn).FirstOrDefault();

        public IQueryable<ApplicationUser> GetAll() => this.repository.All();

        public async Task<ApplicationUser> GetByIdWithDeletedAsync(string id) => await this.repository.GetByIdWithDeletedAsync(id);

        public async Task UploadAvatarImgPathFromLink(string userId, string newImgPath)
        {
            var user = await this.GetByIdWithDeletedAsync(userId);

            user.AvatarImgSrc = newImgPath;

            await this.repository.SaveChangesAsync();
        }

        public bool IsUserAllowedToSubmitOrder(string userId, OrderInputModel input)
        {
            if (this.db.Users.Where(x => x.Id == userId).SelectMany(x => x.Orders).Any(x => x.Status != Data.Models.Enumerators.OrderStatus.Completed))
            {
                return false;
            }

            return true;
        }
    }
}

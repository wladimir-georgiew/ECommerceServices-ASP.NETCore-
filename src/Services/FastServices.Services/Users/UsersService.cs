namespace FastServices.Services.Users
{
    using System;
    using System.Linq;

    using FastServices.Data;
    using FastServices.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
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

        public Comment GetUserLatestComment(string userId) => this.db.Comments.Where(x => x.ApplicationUserId == userId).OrderByDescending(x => x.CreatedOn).FirstOrDefault();
    }
}

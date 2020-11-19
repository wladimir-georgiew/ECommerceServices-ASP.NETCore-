namespace FastServices.Services.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface IUsersService
    {
        public Comment GetUserLatestComment(string userId);

        public bool IsUserAllowedToComment(string userId);

        public IQueryable<ApplicationUser> GetAll();

        public Task<ApplicationUser> GetByIdWithDeletedAsync(string id);

        public Task UploadAvatarImgPathFromLink(string userId, string newImgPath);
    }
}

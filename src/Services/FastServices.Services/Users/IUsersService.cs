using FastServices.Web.ViewModels.Employees;

namespace FastServices.Services.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Orders;

    public interface IUsersService
    {
        public Comment GetUserLatestComment(string userId);

        public bool IsUserAllowedToComment(string userId);

        public IQueryable<ApplicationUser> GetAll();

        public Task<ApplicationUser> GetByIdWithDeletedAsync(string id);

        public Task UploadAvatarImgPathFromLink(string userId, string newImgPath);

        public bool IsUserAllowedToSubmitOrder(string userId, OrderInputModel input);

        public Task AssignUserToRoleAsync(string roleName, ApplicationUser user);

        public Task<ApplicationUser> CreateUserAsync(EmployeeInputModel model, string uniqueFileName);
    }
}

using FastServices.Web.ViewModels.Administration;
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

        public Task UpdateUserAvatarImg(string userId, string uniqueFileName);

        public bool IsUserAllowedToSubmitOrder(string userId);

        public Task AssignUserToRoleAsync(string roleName, ApplicationUser user);

        public Task<ApplicationUser> CreateUserAsync(EmployeeInputModel model, string uniqueFileName);
    }
}

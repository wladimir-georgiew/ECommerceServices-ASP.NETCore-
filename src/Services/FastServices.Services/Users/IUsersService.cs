namespace FastServices.Services.Users
{
    using FastServices.Data.Models;

    public interface IUsersService
    {
        public Comment GetUserLatestComment(string userId);

        public bool IsUserAllowedToComment(string userId);
    }
}

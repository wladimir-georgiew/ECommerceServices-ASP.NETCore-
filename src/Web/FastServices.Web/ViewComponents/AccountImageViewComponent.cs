namespace FastServices.Web.ViewComponents
{
    using System.Linq;

    using FastServices.Data;
    using Microsoft.AspNetCore.Mvc;

    public class AccountImageViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public AccountImageViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var userName = this.User.Identity.Name;

            var user = this.db.Users.Where(x => x.Email == userName).FirstOrDefault();

            string accountImgSrc = user.AvatarImgSrc;

            return this.View(model: new string[] { accountImgSrc, user.Name });
        }
    }
}

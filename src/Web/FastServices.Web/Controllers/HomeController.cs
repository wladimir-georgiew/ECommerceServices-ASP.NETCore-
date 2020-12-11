namespace FastServices.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using FastServices.Common;
    using FastServices.Services.Departments;
    using FastServices.Services.Messaging;
    using FastServices.Web.ViewModels;
    using FastServices.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IDepartmentsService departmentServices;
        private readonly IEmailSender emailSender;

        public HomeController(IDepartmentsService departmentServices, IEmailSender emailSender)
        {
            this.departmentServices = departmentServices;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            List<IndexViewModel> model = this.departmentServices
                .GetAllDepartments()
                .Select(x => new IndexViewModel
            {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CardImgSrc = x.CardImgSrc,
                    Rating = this.departmentServices.GetDepartmentRatingById(x.Id),
            })
                .ToList();

            this.ViewData["topImageNavUrl"] = "/Template/images/indexBg-1.jpg";

            return this.View(model);
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactInputModel input)
        {
            await this.emailSender.SendEmailAsync($"sneakypeekymustard@gmail.com", $"{input.Email}", "vladimir1.dev@gmail.com", $"ContactForm by {input.Name}", $"{input.Message}");

            this.TempData["msg"] = GlobalConstants.SuccessContactEmailMessage;

            return this.View();
        }

        public IActionResult Error(string code)
        {
            if (code == "404")
            {
                return this.View();
            }

            return this.RedirectToAction("ErrorOld");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorOld()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

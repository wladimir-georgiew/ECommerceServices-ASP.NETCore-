namespace FastServices.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using FastServices.Services.Departments;
    using FastServices.Web.ViewModels;
    using FastServices.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IDepartmentsService departmentServices;

        public HomeController(IDepartmentsService departmentServices)
        {
            this.departmentServices = departmentServices;
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

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult Gallery()
        {
            return this.View();
        }

        public IActionResult Services()
        {
            return this.RedirectToAction("Error");
        }

        public IActionResult Typography()
        {
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

namespace FastServices.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using FastServices.Services.Departments;
    using FastServices.Web.ViewModels;
    using FastServices.Web.ViewModels.Home;
    using HomeServices.Data.Models;
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
            List<DepartmentViewModel> departmentsViewModel = this.departmentServices
                .GetAllDepartments()
                .Select(x => new DepartmentViewModel
            {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CardImgSrc = x.CardImgSrc,
            })
                .ToList();

            return this.View(departmentsViewModel);
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

        public IActionResult Test()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return this.View(
        //        new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        //}
    }
}

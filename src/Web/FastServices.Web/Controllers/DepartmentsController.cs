namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Services.Services;
    using FastServices.Web.ViewModels.Departments;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentsController : Controller
    {
        private readonly IServicesService servicesService;
        private readonly ApplicationDbContext db;

        public DepartmentsController(IServicesService servicesService, ApplicationDbContext db)
        {
            this.servicesService = servicesService;
            this.db = db;
        }

        public IActionResult Department(int id)
        {
            List<ServicesViewModel> servicesViewModel = this.servicesService.GetDepartmentServices(id)
                .Select(x => new ServicesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CardImgSrc = x.CardImgSrc,
                    DepartmentImgSrc = this.db.Departments.FirstOrDefault(x => x.Id == id).BackgroundImgSrc,
                })
                .ToList();

            return this.View(servicesViewModel);
        }
    }
}

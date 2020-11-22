namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Services.Departments;
    using FastServices.Services.Services;
    using FastServices.Services.Users;
    using FastServices.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ServiceController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly IUsersService usersService;
        private readonly IServicesService servicesService;

        public ServiceController(IDepartmentsService departmentsService, IUsersService usersService, IServicesService servicesService)
        {
            this.departmentsService = departmentsService;
            this.usersService = usersService;
            this.servicesService = servicesService;
        }

        [Authorize]
        public async Task<IActionResult> Service(int serviceId)
        {
            var service = await this.servicesService.GetByIdWtihDeletedAsync(serviceId);
            var department = await this.departmentsService.GetDepartmentByIdAsync(service.DepartmentId);

            this.ViewData["topImageNavUrl"] = department.BackgroundImgSrc;
            this.ViewData["title"] = service.Name;
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Service(OrderInputModel input)
        {
            var service = await this.servicesService.GetByIdWtihDeletedAsync(input.ServiceId);
            var department = await this.departmentsService.GetDepartmentByIdAsync(service.DepartmentId);

            this.ViewData["topImageNavUrl"] = department.BackgroundImgSrc;
            this.ViewData["title"] = service.Name;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.View();
        }
    }
}

namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FastServices.Services.Departments;
    using FastServices.Services.Orders;
    using FastServices.Services.Services;
    using FastServices.Services.Users;
    using FastServices.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ServiceController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly IServicesService servicesService;
        private readonly IOrdersService ordersService;

        public ServiceController(IDepartmentsService departmentsService, IServicesService servicesService, IOrdersService ordersService)
        {
            this.departmentsService = departmentsService;
            this.servicesService = servicesService;
            this.ordersService = ordersService;
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

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.Price = ((Common.GlobalConstants.HourlyFeePerWorker * input.WorkersCount) * input.HoursBooked) + service.Fee;

            await this.ordersService.AddOrderAsync(input, userId, department.Id);

            return this.View();
        }
    }
}

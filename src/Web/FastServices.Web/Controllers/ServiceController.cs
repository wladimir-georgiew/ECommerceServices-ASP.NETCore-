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
        private readonly IUsersService usersService;

        public ServiceController(IDepartmentsService departmentsService, IServicesService servicesService, IOrdersService ordersService, IUsersService usersService)
        {
            this.departmentsService = departmentsService;
            this.servicesService = servicesService;
            this.ordersService = ordersService;
            this.usersService = usersService;
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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.usersService.GetByIdWithDeletedAsync(userId);

            this.ViewData["topImageNavUrl"] = department.BackgroundImgSrc;
            this.ViewData["title"] = service.Name;

            input.Price = ((Common.GlobalConstants.HourlyFeePerWorker * input.WorkersCount) * input.HoursBooked) + service.Fee;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.usersService.IsUserAllowedToSubmitOrder(userId, input))
            {
                this.ModelState.AddModelError(string.Empty, "You are allowed to have one active order at a time");
                return this.View(input);
            }

            if (!await this.ordersService.AddOrderAsync(input, user, department.Id))
            {
                this.ModelState.AddModelError(string.Empty, "There are currently no available employees for this date. Try again with different date.");
                return this.View(input);
            }

            this.TempData["msg"] = "Successfully submitted your order!";
            return this.View();
        }
    }
}

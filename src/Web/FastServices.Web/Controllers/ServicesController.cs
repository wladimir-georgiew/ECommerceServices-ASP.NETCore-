namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Data.Models;
    using FastServices.Services.Departments;
    using FastServices.Services.Orders;
    using FastServices.Services.Services;
    using FastServices.Services.Users;
    using FastServices.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ServicesController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly IServicesService servicesService;
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ServicesController(
            IDepartmentsService departmentsService,
            IServicesService servicesService,
            IOrdersService ordersService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.departmentsService = departmentsService;
            this.servicesService = servicesService;
            this.ordersService = ordersService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Service(int serviceId)
        {
            var service = await this.servicesService.GetByIdWithDeletedAsync(serviceId);

            if (service == null)
            {
                return this.NotFound();
            }

            var department = await this.departmentsService.GetDepartmentByIdAsync(service.DepartmentId);

            this.ViewData["topImageNavUrl"] = department.BackgroundImgSrc;
            this.ViewData["title"] = service.Name;
            return this.View();
        }

        // Add Order
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Service(OrderInputModel input)
        {
            var service = await this.servicesService.GetByIdWithDeletedAsync(input.ServiceId);
            var department = await this.departmentsService.GetDepartmentByIdAsync(service.DepartmentId);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.usersService.GetByIdWithDeletedAsync(userId);

            this.ViewData["topImageNavUrl"] = department.BackgroundImgSrc;
            this.ViewData["title"] = service.Name;

            input.Price = ((GlobalConstants.HourlyFeePerWorker * input.WorkersCount) * input.HoursBooked) + service.Fee;

            if (this.userManager.GetRolesAsync(user).GetAwaiter().GetResult().Contains(GlobalConstants.EmployeeRoleName))
            {
                this.ModelState.AddModelError(string.Empty, GlobalConstants.ErrorEmployeeSubmitOrder);
                return this.View(input);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.usersService.IsUserAllowedToSubmitOrder(userId, input))
            {
                this.ModelState.AddModelError(string.Empty, GlobalConstants.ErrorOrderSubmitOneOrderAtATime);
                return this.View(input);
            }

            if (!await this.ordersService.AddOrderAsync(input, user, department.Id))
            {
                this.ModelState.AddModelError(string.Empty, GlobalConstants.ErrorOrderNotEnoughAvailableEmployees);
                return this.View(input);
            }

            this.TempData["msg"] = GlobalConstants.SuccessOrderSubmitted;
            return this.View();
        }
    }
}

namespace FastServices.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Data.Models;
    using FastServices.Services.Orders;
    using FastServices.Services.Users;
    using FastServices.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrdersService ordersService, UserManager<ApplicationUser> userManager)
        {
            this.ordersService = ordersService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(string role)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.ViewData["topImageNavUrl"] = "/Template/images/indexBg-1.jpg";

            var user = await this.userManager.FindByIdAsync(userId);
            var roles = await this.userManager.GetRolesAsync(user);

            var orders = roles.Contains(GlobalConstants.EmployeeRoleName)
                ? this.ordersService.GetEmployeeOrders(userId)
                : this.ordersService.GetUserOrders(userId);

            var model = orders
                        .OrderBy(x => x.StartDate)
                        .Select(x => new OrderViewModel
                        {
                            Address = x.Address,
                            WorkersCount = x.WorkersCount,
                            StartDate = x.StartDate.ToString("f"),
                            DueDate = x.DueDate.ToString("f"),
                            HoursBooked = x.BookedHours,
                            ServiceName = x.Service.Name,
                            Status = x.Status.ToString(),
                            Price = x.Price,
                        })
                        .ToList();

            return this.View("Orders", model);
        }
    }
}

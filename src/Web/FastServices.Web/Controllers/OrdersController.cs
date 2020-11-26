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
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [Authorize(Roles = GlobalConstants.EmployeeRoleName)]
        public IActionResult EmployeeOrders()
        {
            this.ViewData["topImageNavUrl"] = "/Template/images/indexBg-1.jpg";
            return this.View("Orders");
        }

        public IActionResult UserOrders()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.ViewData["topImageNavUrl"] = "/Template/images/indexBg-1.jpg";

            var allOrders = this.ordersService.GetUserOrders(userId)
                .Select(x => new OrderViewModel
            {
                    Address = x.Address,
                    WorkersCount = x.WorkersCount,
                    StartDate = x.StartDate.ToString("D"),
                    DueDate = x.DueDate.ToString("D"),
                    HoursBooked = x.BookedHours,
                    ServiceName = x.Service.Name,
                    Status = x.Status.ToString(),
            })
                .ToList();

            return this.View("Orders", allOrders);
        }
    }
}

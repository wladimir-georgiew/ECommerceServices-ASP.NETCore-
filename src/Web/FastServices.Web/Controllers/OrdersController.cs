namespace FastServices.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Data.Models;
    using FastServices.Services.Complaints;
    using FastServices.Services.Orders;
    using FastServices.Services.Services;
    using FastServices.Services.Users;
    using FastServices.Web.ViewModels.Complaint;
    using FastServices.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IServicesService servicesService;
        private readonly IComplaintsService complaintsService;

        public OrdersController(
            IOrdersService ordersService,
            IServicesService servicesService,
            UserManager<ApplicationUser> userManager,
            IComplaintsService complaintsService)
        {
            this.ordersService = ordersService;
            this.userManager = userManager;
            this.servicesService = servicesService;
            this.complaintsService = complaintsService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await this.userManager.FindByIdAsync(userId);
            var roles = await this.userManager.GetRolesAsync(user);

            var orders = roles.Contains(GlobalConstants.EmployeeRoleName)
                ? this.ordersService.GetEmployeeOrders(userId)
                : this.ordersService.GetUserOrders(userId);

            var model = orders
                        .OrderBy(x => x.StartDate)
                        .Select(x => new OrderViewModel
                        {
                            Id = x.Id,
                            Address = x.Address,
                            WorkersCount = x.WorkersCount,
                            StartDate = x.StartDate.ToString("f"),
                            DueDate = x.DueDate.ToString("f"),
                            HoursBooked = x.BookedHours,
                            ServiceName = x.Service.Name,
                            Status = x.Status.ToString(),
                            Price = x.Price,
                            CardImgSrc = this.servicesService.GetByIdWithDeletedAsync(x.Service.Id).GetAwaiter().GetResult().CardImgSrc,
                            HasComplaint = this.ordersService.GetComplaints(x.Id).Any(),
                        })
                        .ToList();

            return this.View("Orders", model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComplaint(ComplaintInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["msg"] = GlobalConstants.ErrorComplaintSubmitted;
                return this.Redirect($"/Orders/All?searchOption=2");
            }

            var order = this.ordersService.GetByIdWithDeleted(input.OrderId);

            await this.complaintsService.AddComplaint(order, input);

            this.TempData["msg"] = GlobalConstants.SuccessComplaintSubmitted;

            return this.Redirect($"/Orders/All?searchOption=2");
        }
    }
}

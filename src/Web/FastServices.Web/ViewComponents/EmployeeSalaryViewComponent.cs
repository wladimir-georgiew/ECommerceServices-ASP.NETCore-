namespace FastServices.Web.ViewComponents
{
    using System;
    using System.Globalization;
    using System.Linq;

    using FastServices.Data;
    using FastServices.Services.Orders;
    using Microsoft.AspNetCore.Mvc;

    public class EmployeeSalaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;
        private readonly IOrdersService ordersService;

        public EmployeeSalaryViewComponent(ApplicationDbContext db, IOrdersService ordersService)
        {
            this.db = db;
            this.ordersService = ordersService;
        }

        public IViewComponentResult Invoke()
        {
            var userName = this.User.Identity.Name;

            var user = this.db.Users.Where(x => x.Email == userName).FirstOrDefault();

            var employee = this.db.Employees.FirstOrDefault(x => x.ApplicationUserId == user.Id);

            var employeeOrders = this.ordersService
                        .GetEmployeeOrders(employee.Id)
                        .Where(x => x.Status == Data.Models.Enumerators.OrderStatus.Completed)
                        .ToList();

            var bonusOnCompletedOrders = employeeOrders.Sum(x => x.Price * 0.01M);

            var englishCulture = CultureInfo.CurrentCulture = new CultureInfo("en-US");

            return this.View(model: new string[]
            {
                employee.Salary.ToString("f2"),
                bonusOnCompletedOrders.ToString("f2"),
                DateTime.UtcNow.ToString("MMMM", provider: englishCulture),
            });
        }
    }
}

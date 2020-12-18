namespace FastServices.Web.Quartz.Jobs.Orders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Services.Orders;
    using global::Quartz;
    using Microsoft.Extensions.DependencyInjection;

    public class IncreaseEmployeeSalary : IJob
    {
        private readonly IServiceProvider provider;

        public IncreaseEmployeeSalary(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Create a new scope
            using (var scope = this.provider.CreateScope())
            {
                // Resolve the Scoped service
                var ordersService = scope.ServiceProvider.GetService<IOrdersService>();

                // Increase employee salary EVERY MONTH by 0.3% of the total price for each order they have ever completed
                var completedOrders = ordersService
                    .GetAll()
                    .Where(x => x.Status == Data.Models.Enumerators.OrderStatus.Completed);

                // Only employees with completed orders
                var employees = completedOrders.SelectMany(x => x.EmployeesOrder.Select(e => e.Employee)).Distinct().ToList();

                foreach (var employee in employees)
                {
                    // Only EmployeeOrders which are completed
                    var employeeOrders = ordersService
                        .GetEmployeeOrdersByEmployeeId(employee.Id)
                        .Where(x => x.Status == Data.Models.Enumerators.OrderStatus.Completed)
                        .ToList();

                    foreach (var order in employeeOrders)
                    {
                        employee.Salary += order.Price * 0.003M;
                    }
                }

                await ordersService.SaveChangesAsync();
            }
        }
    }
}

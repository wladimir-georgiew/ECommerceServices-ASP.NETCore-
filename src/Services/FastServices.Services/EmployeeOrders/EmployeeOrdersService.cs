namespace FastServices.Services.EmployeeOrders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Data.Models.Enumerators;
    using FastServices.Services.Employees;

    public class EmployeeOrdersService : IEmployeeOrdersService
    {
        private readonly IEmployeesService employeesService;
        private readonly IRepository<EmployeeOrder> repository;

        public EmployeeOrdersService(IEmployeesService employeesService, IRepository<EmployeeOrder> repository)
        {
            this.employeesService = employeesService;
            this.repository = repository;
        }

        public async Task AssignEmployeesToOrderAsync(Order order, List<Employee> availableEmployees)
        {
            for (int i = 0; i < order.WorkersCount; i++)
            {
                var currEmpl = await this.employeesService.GetByIdWithDeletedAsync(availableEmployees[i].Id);

                EmployeeOrder emplOrder = new EmployeeOrder
                {
                    EmployeeId = currEmpl.Id,
                    OrderId = order.Id,
                };

                order.EmployeesOrder.Add(emplOrder);
                currEmpl.EmployeeOrders.Add(emplOrder);

                currEmpl.IsAvailable = false;

                await this.repository.AddAsync(emplOrder);
            }

            order.Status = OrderStatus.Ongoing;
        }
    }
}

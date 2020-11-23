namespace FastServices.Services.Orders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Data.Models.Enumerators;
    using FastServices.Services.EmployeeOrders;
    using FastServices.Services.Employees;
    using FastServices.Web.ViewModels.Orders;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> repository;
        private readonly IEmployeeOrdersService employeeOrdersService;
        private readonly IEmployeesService employeesService;

        public OrdersService(IRepository<Order> repository, IEmployeeOrdersService employeeOrdersService, IEmployeesService employeesService)
        {
            this.repository = repository;
            this.employeeOrdersService = employeeOrdersService;
            this.employeesService = employeesService;
        }

        public Order GetByIdWithDeletedAsync(string id) => this.repository.All().ToList().FirstOrDefault(x => x.Id == id);

        public async Task<bool> AddOrderAsync(OrderInputModel model, ApplicationUser user, int departmentId)
        {
            // available employees of the current department
            //var availableEmployees = this.employeesService.GetAll().Where(x => x.IsAvailable && x.DepartmentId == departmentId).ToList();

            var availableEmployees = this.employeesService.GetAllAvailableEmployees(departmentId, model.DateDate, model.DueDate);

            var order = new Order
            {
                ApplicationUserId = user.Id,
                BookedHours = model.HoursBooked,
                WorkersCount = model.WorkersCount,
                SubmitDate = DateTime.UtcNow,
                StartDate = model.StartDate,
                DueDate = model.DueDate,
                ServiceId = model.ServiceId,
                Price = model.Price,
                Status = OrderStatus.Pending,
            };

            //await this.AssignEmployeesToOrderAsync(order, availableEmployees);

            if (order.Status.ToString() == "Pending" &&
               availableEmployees.Count >= order.WorkersCount)
            {
                await this.employeeOrdersService.AssignEmployeesToOrderAsync(order, availableEmployees);
            }
            else
            {
                return false;
            }

            user.Orders.Add(order);
            await this.repository.AddAsync(order);
            await this.repository.SaveChangesAsync();

            return true;
        }

        //public async Task AssignEmployeesToOrderAsync(Order order, List<Employee> availableEmployees)
        //{
        //    if (order.Status.ToString() == "Pending" &&
        //        availableEmployees.Count >= order.WorkersCount)
        //    {
        //        for (int i = 0; i < order.WorkersCount; i++)
        //        {
        //            var currEmpl = await this.employeesService.GetByIdWithDeletedAsync(availableEmployees[i].Id);

        //            EmployeeOrder emplOrder = new EmployeeOrder
        //            {
        //                EmployeeId = currEmpl.Id,
        //                OrderId = order.Id,
        //            };

        //            order.EmployeesOrder.Add(emplOrder);
        //            currEmpl.EmployeeOrders.Add(emplOrder);

        //            currEmpl.IsAvailable = false;
        //        }
        //    }

        //    order.Status = OrderStatus.Ongoing;
        //}
    }
}

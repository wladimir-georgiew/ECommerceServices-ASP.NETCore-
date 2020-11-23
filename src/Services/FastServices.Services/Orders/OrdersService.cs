namespace FastServices.Services.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Data.Models.Enumerators;
    using FastServices.Services.EmployeeOrders;
    using FastServices.Services.Employees;
    using FastServices.Services.Services;
    using FastServices.Web.ViewModels.Orders;

    public class OrdersService : IOrdersService
    {
        private const decimal HourlyFeePerWorker = 5;

        private readonly IRepository<Order> repository;
        private readonly IEmployeeOrdersService employeeOrdersService;
        private readonly IEmployeesService employeesService;

        public OrdersService(IRepository<Order> repository, IEmployeeOrdersService employeeOrdersService, IEmployeesService employeesService)
        {
            this.repository = repository;
            this.employeeOrdersService = employeeOrdersService;
            this.employeesService = employeesService;
        }

        public async Task AddOrderAsync(OrderInputModel model, string userId, int departmentId)
        {
            // available employees of the current department
            var availableEmployees = this.employeesService.GetAll().Where(x => x.IsAvailable && x.DepartmentId == departmentId).ToList();

            var order = new Order
            {
                ApplicationUserId = userId,
                BookedHours = model.HoursBooked,
                WorkersCount = model.WorkersCount,
                SubmitDate = DateTime.UtcNow,
                StartDate = model.Date,
                DueDate = model.Date.AddHours(model.HoursBooked),
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

            await this.repository.AddAsync(order);
            await this.repository.SaveChangesAsync();
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

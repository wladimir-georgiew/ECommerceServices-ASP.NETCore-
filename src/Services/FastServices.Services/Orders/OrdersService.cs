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

        public async Task SaveChangesAsync() => await this.repository.SaveChangesAsync();

        public IQueryable<Order> GetAll() => this.repository.All();

        public Order GetUserActiveOrder(string userId)
        {
            var orders = this.repository.All().Where(x => x.ApplicationUserId == userId);

            return orders
                        .Where(x => x.Status == OrderStatus.Active)
                        .FirstOrDefault();
        }

        public IQueryable<Order> GetUserOrders(string userId)
        {
            var orders = this.repository.All()
                .Where(x => x.ApplicationUserId == userId);

            return orders;
        }

        public IQueryable<Order> GetEmployeeOrdersByUserId(string userId)
        {
            var employee = this.employeesService.GetByUserId(userId);
            var orders = this.repository.All()
                .Where(x => x.EmployeesOrder
                .Any(e => e.EmployeeId == employee.Id));

            return orders;
        }

        public IQueryable<Order> GetEmployeeOrders(string employeeId)
        {
            var orders = this.repository.All()
                .Where(x => x.EmployeesOrder
                .Any(e => e.EmployeeId == employeeId));

            return orders;
        }

        public Order GetByIdWithDeleted(string id) => this.repository.All().ToList().FirstOrDefault(x => x.Id == id);

        public async Task AddAsync(Order order, List<Employee> availableEmployees, ApplicationUser user)
        {
            order.ApplicationUserId = user.Id;
            user.Orders.Add(order);
            await this.employeeOrdersService.AssignEmployeesToOrderAsync(order, availableEmployees);

            await this.repository.AddAsync(order);
            await this.repository.SaveChangesAsync();
        }

        public Order GetOrderFromInputModel(OrderInputModel model)
        {
            var order = new Order
            {
                BookedHours = model.HoursBooked,
                WorkersCount = model.WorkersCount,
                SubmitDate = DateTime.UtcNow,
                StartDate = model.StartDate.ToUniversalTime(),
                DueDate = model.DueDate.ToUniversalTime(),
                ServiceId = model.ServiceId,
                Status = OrderStatus.Undefined,
                PaymentMethod = "Cash",
                Address = model.Address,
            };

            return order;
        }

        public bool HasAvailableEmployeesForTheOrderAsync(List<Employee> availableEmployees, Order order)
        {
            if (availableEmployees.Count < order.WorkersCount)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Complaint> GetComplaints(string orderId)
            => this.repository.All().Where(x => x.Id == orderId).Select(x => x.Complaints).FirstOrDefault();

        public async Task ChangeOrderPayment(string orderId, string paymentMethod)
        {
            var order = this.GetByIdWithDeleted(orderId);
            order.PaymentMethod = paymentMethod;
            await this.SaveChangesAsync();
        }
    }
}

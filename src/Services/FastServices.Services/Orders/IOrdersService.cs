namespace FastServices.Services.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        //public Task<bool> AddOrderAsync(OrderInputModel model, ApplicationUser user, int departmentId);

        public Order GetByIdWithDeleted(string id);

        public Order GetUserActiveOrder(string userId);

        public IQueryable<Order> GetUserOrders(string userId);

        public Task SaveChangesAsync();

        public IQueryable<Order> GetAll();

        public IQueryable<Order> GetEmployeeOrders(string employeeId);

        public IQueryable<Order> GetEmployeeOrdersByUserId(string userId);

        public IEnumerable<Complaint> GetComplaints(string orderId);

        public Task AddAsync(Order order, List<Employee> availableEmployees, ApplicationUser user);

        public Order GetOrderFromInputModel(OrderInputModel model);

        public bool HasAvailableEmployeesForTheOrderAsync(List<Employee> availableEmployees, Order order);

        public Task ChangeOrderPayment(string orderId, string paymentStatus);
    }
}

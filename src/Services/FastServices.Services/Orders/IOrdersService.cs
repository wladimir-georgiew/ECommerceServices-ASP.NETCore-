namespace FastServices.Services.Orders
{
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        public Task<bool> AddOrderAsync(OrderInputModel model, ApplicationUser user, int departmentId);

        public Order GetByIdWithDeletedAsync(string id);

        public Order GetUserActiveOrder(string userId);

        public IQueryable<Order> GetUserOrders(string userId);
    }
}

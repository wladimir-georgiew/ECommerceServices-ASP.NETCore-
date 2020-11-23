namespace FastServices.Services.Orders
{
    using System.Threading.Tasks;

    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        public Task<bool> AddOrderAsync(OrderInputModel model, ApplicationUser user, int departmentId);

        public Order GetByIdWithDeletedAsync(string id);
    }
}

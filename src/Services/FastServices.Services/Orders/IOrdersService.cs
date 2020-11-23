namespace FastServices.Services.Orders
{
    using System.Threading.Tasks;

    using FastServices.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        public Task AddOrderAsync(OrderInputModel model, string userId, int departmentId);
    }
}

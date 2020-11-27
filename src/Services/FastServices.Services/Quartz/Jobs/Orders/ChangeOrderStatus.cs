namespace FastServices.Web.Quartz.Jobs.Orders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Services.Orders;
    using global::Quartz;
    using Microsoft.Extensions.DependencyInjection;

    public class ChangeOrderStatus : IJob
    {
        private readonly IServiceProvider provider;

        public ChangeOrderStatus(IServiceProvider provider)
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

                var ordersToComplete = ordersService.GetAll().Where(x => x.DueDate <= DateTime.UtcNow);

                foreach (var order in ordersToComplete)
                {
                    order.Status = Data.Models.Enumerators.OrderStatus.Completed;
                }

                await ordersService.SaveChangesAsync();
            }
        }
    }
}

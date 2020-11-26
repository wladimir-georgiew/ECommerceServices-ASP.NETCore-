namespace FastServices.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class OrderMasterViewModel
    {
        public OrderViewModel ActiveOrder { get; set; }

        public IEnumerable<OrderViewModel> HistoryOrders { get; set; }
    }
}

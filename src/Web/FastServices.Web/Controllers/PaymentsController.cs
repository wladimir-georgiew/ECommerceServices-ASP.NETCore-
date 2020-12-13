namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Services.Orders;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;

    public class PaymentsController : Controller
    {
        private readonly IOrdersService ordersService;

        public PaymentsController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost]
        public async Task<IActionResult> Processing(string stripeToken, string stripeEmail, decimal orderPrice, string orderId)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = await customers.CreateAsync(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken,
            });

            var charge = await charges.CreateAsync(new ChargeCreateOptions
            {
                Amount = (long)orderPrice * 100,
                Description = $"Order Id = {orderId}",
                Currency = "usd",
                Customer = customer.Id,
            });

            // Failed transaction
            if (charge.Status == "failed")
            {
                this.TempData["msg"] = GlobalConstants.ErrorPayment;
                return this.NoContent();
            }

            // Successful transaction
            await this.ordersService.ChangeOrderPayment(orderId, "CreditCard");
            this.TempData["msg"] = GlobalConstants.SuccessPayment;

            return this.RedirectToAction("All", "Orders", new { searchOption = 1 });
        }
    }
}

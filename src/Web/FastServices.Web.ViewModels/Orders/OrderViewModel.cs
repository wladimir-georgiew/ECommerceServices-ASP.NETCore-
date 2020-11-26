namespace FastServices.Web.ViewModels.Orders
{
    using System;

    public class OrderViewModel
    {
        public string ServiceName { get; set; }

        public string Address { get; set; }

        public string StartDate { get; set; }

        public string DueDate { get; set; }

        public int HoursBooked { get; set; }

        public int WorkersCount { get; set; }

        public string Status { get; set; }
    }
}

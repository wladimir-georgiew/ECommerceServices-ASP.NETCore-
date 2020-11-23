namespace FastServices.Web.ViewModels.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FastServices.Web.Infrastructure.Attributes;

    public class OrderInputModel
    {
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "The date field is required")]
        [MyDate(ErrorMessage = "Invalid date")]
        public DateTime DateDate { get; set; }

        [Range(9, 16)]
        public double DateHour { get; set; }

        public DateTime StartDate => this.DateDate.AddHours(this.DateHour);

        public DateTime DueDate => this.StartDate.AddHours(this.HoursBooked);

        [Range(1, 8)]
        public int HoursBooked { get; set; }

        [Range(1, 4)]
        public int WorkersCount { get; set; }

        public decimal Price { get; set; }
    }
}

namespace HomeServices.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FastServices.Data.Models;
    using HomeServices.Data.Models.Enumerators;

    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.EmployeesOrder = new HashSet<EmployeeOrder>();

            // TODO Add to order services
            //SubmitDate = DateTime.UtcNow;
            //DueDate = this.StartDate.AddHours(this.BookedHours);
            //Price = (Service.Fee * BookedHours) * WorkersCount;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        [Required]
        public int WorkersCount { get; set; }

        [Required]
        public int BookedHours { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime SubmitDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<EmployeeOrder> EmployeesOrder { get; set; }
    }
}

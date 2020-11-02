namespace HomeServices.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Complaint
    {
        public Complaint()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
    }
}

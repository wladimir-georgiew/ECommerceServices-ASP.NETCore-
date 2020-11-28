namespace FastServices.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FastServices.Data.Common.Models;

    public class Complaint : IAuditInfo, IDeletableEntity
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

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

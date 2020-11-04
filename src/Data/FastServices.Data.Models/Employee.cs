namespace HomeServices.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FastServices.Data.Common.Models;
    using FastServices.Data.Models;

    public class Employee : IAuditInfo, IDeletableEntity
    {
        public Employee()
        {
            this.Id = Guid.NewGuid().ToString();
            this.EmployeeOrders = new HashSet<EmployeeOrder>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public decimal Salary { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<EmployeeOrder> EmployeeOrders { get; set; }
    }
}

namespace HomeServices.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FastServices.Data.Common.Models;

    public class Service : IDeletableEntity, IAuditInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Department Department { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public decimal Fee { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

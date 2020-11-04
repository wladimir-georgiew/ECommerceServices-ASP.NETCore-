namespace HomeServices.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FastServices.Data.Common.Models;
    using FastServices.Data.Common.Repositories;

    public class Department : IDeletableEntity, IAuditInfo
    {
        public Department()
        {
            this.Services = new HashSet<Service>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}

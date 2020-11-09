namespace FastServices.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FastServices.Data.Common.Models;

    public class Department : IDeletableEntity, IAuditInfo
    {
        public Department()
        {
            this.Services = new HashSet<Service>();

            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BackgroundImgSrc { get; set; }

        [Required]
        public string CardImgSrc { get; set; }

        [Required]
        public string Description { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

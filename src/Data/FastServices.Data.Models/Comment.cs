namespace FastServices.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FastServices.Data.Common.Models;

    public class Comment : IDeletableEntity, IAuditInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CommentContent { get; set; }

        public int Stars { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

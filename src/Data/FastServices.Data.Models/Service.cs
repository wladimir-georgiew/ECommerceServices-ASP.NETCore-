namespace HomeServices.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Service
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Department Department { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public decimal Fee { get; set; }
    }
}

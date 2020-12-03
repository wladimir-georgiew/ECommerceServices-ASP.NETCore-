using System;

namespace FastServices.Web.ViewModels.Services
{
    using System.ComponentModel.DataAnnotations;

    using FastServices.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    public class ServiceInputModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Fee is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Min fee value is 1")]
        public decimal Fee { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(20, ErrorMessage = "Min length is 20 characters")]
        [MaxLength(300, ErrorMessage = "Max length is 300 characters")]
        public string Description { get; set; }

        [Display(Name = "Profile Picture")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".jpg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)] // 3mb
        public IFormFile BackgroundImage { get; set; }
    }
}

namespace FastServices.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using FastServices.Web.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    public class DepartmentInputModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Background image is required")]
        [Display(Name = "Background Image")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".jpg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)] // 3mb
        public IFormFile BackgroundImage { get; set; }

        [Display(Name = "Card Image")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".jpg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)] // 3mb
        public IFormFile CardImage { get; set; }
    }
}

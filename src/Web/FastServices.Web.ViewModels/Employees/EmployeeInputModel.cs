using FastServices.Web.Infrastructure.Attributes;

namespace FastServices.Web.ViewModels.Employees
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class EmployeeInputModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MinLength(3, ErrorMessage = "First name min length is 6 symbols")]
        [MaxLength(15, ErrorMessage = "First name max length is 20 symbols")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [MinLength(3, ErrorMessage = "Last name min length is 6 symbols")]
        [MaxLength(15, ErrorMessage = "Last name max length is 20 symbols")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "Password min length is 6 symbols")]
        [MaxLength(20, ErrorMessage = "Password max length is 20 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Phone]
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }

        [Display(Name = "Profile Picture")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".jpg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)] // 3mb
        public IFormFile ProfileImage { get; set; }
    }
}

namespace FastServices.Web.ViewModels.Departments
{
    using System.ComponentModel.DataAnnotations;

    public class OrderModalViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Your email address is required.")]
        [EmailAddress(ErrorMessage = "Incorrect email address.")]
        public string EmailAddress { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Your last name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Your last name is too short.")]
        public string LastName { get; set; }

        public string ResultMessage { get; set; }
    }
}

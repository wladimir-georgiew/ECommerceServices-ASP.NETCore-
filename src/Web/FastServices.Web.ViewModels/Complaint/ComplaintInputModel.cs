namespace FastServices.Web.ViewModels.Complaint
{
    using System.ComponentModel.DataAnnotations;

    public class ComplaintInputModel
    {
        public string OrderId { get; set; }

        [Required]
        [MinLength(15)]
        [MaxLength(300)]
        public string Description { get; set; }
    }
}

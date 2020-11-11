namespace FastServices.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Comment should be at least 5 characters long")]
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(100, ErrorMessage = "Comment cannot be more than 100 characters long")]
        public string CommentContent { get; set; }

        [Range(0, 5)]
        public int Stars { get; set; }

        public string UserId { get; set; }
    }
}

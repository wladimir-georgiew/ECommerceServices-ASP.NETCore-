namespace FastServices.Web.ViewModels.Comments
{
    public class CommentInputModel
    {
        public int DepartmentId { get; set; }

        public string CommentContent { get; set; }

        public int Stars { get; set; }

        public string UserId { get; set; }
    }
}

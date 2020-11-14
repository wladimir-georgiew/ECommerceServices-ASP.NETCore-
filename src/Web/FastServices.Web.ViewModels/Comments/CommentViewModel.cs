namespace FastServices.Web.ViewModels.Comments
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        public string CommentContent { get; set; }

        public string Name { get; set; }

        public string CreatedOn { get; set; }

        public int Stars { get; set; }

        public int DepartmentId { get; set; }

        public string AvatarImgSrc { get; set; }

        public string UserId { get; set; }
    }
}

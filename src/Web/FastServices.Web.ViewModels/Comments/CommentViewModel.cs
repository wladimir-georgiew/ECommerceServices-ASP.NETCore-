using FastServices.Data.Models;

namespace FastServices.Web.ViewModels.Comments
{
    public class CommentViewModel
    {
        public string CommentContent { get; set; }

        public string Name { get; set; }

        public string CreatedOn { get; set; }

        public int Stars { get; set; }

        public int DepartmentId { get; set; }

        public string AvatarImgSrc { get; set; }
    }
}

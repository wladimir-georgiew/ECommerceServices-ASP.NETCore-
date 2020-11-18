namespace FastServices.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    public class CommentsMasterModel
    {
        public IEnumerable<CommentViewModel> ViewModel { get; set; }

        public CommentInputModel InputModel { get; set; }
    }
}

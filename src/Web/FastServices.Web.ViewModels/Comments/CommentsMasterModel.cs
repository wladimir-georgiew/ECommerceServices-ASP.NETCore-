namespace FastServices.Web.ViewModels.Comments
{
    using FastServices.Web.ViewModels.PaginationList;
    using System.Collections.Generic;

    public class CommentsMasterModel
    {
        public PaginationList<CommentViewModel> ViewModel { get; set; }

        public CommentInputModel InputModel { get; set; }
    }
}

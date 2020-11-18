namespace FastServices.Web.ViewModels.Departments
{
    using System.Collections.Generic;

    using FastServices.Web.ViewModels.Comments;

    public class DepartmentViewModel
    {
        public IEnumerable<ServiceViewModel> ServicesViewModel { get; set; }

        public CommentsMasterModel CommentsMasterModel { get; set; }
    }
}

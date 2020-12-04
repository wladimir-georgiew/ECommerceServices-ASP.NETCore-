namespace FastServices.Web.ViewModels.Administration
{
    using System.Collections.Generic;

    using FastServices.Web.ViewModels.Departments;

    public class CreateServiceMasterModel
    {
        public ICollection<SharedDepartmentViewModel> Departments { get; set; }

        public ServiceInputModel InputModel { get; set; }
    }
}

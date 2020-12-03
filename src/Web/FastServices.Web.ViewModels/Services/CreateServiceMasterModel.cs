namespace FastServices.Web.ViewModels.Services
{
    using System.Collections.Generic;

    using FastServices.Web.ViewModels.Departments;
    using FastServices.Web.ViewModels.Employees;

    public class CreateServiceMasterModel
    {
        public ICollection<SharedDepartmentViewModel> Departments { get; set; }

        public ServiceInputModel InputModel { get; set; }
    }
}

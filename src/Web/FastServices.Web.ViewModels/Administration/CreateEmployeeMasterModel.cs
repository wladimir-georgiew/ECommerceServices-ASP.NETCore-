namespace FastServices.Web.ViewModels.Administration
{
    using System.Collections.Generic;

    using FastServices.Web.ViewModels.Departments;

    public class CreateEmployeeMasterModel
    {
        public ICollection<SharedDepartmentViewModel> Departments { get; set; }

        public EmployeeInputModel InputModel { get; set; }
    }
}

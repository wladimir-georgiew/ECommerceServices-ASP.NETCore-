namespace FastServices.Web.ViewModels.Employees
{
    using System.Collections.Generic;

    public class NewEmployeeMasterModel
    {
        public ICollection<EmployeeDepartmentViewModel> Departments { get; set; }

        public EmployeeInputModel InputModel { get; set; }
    }
}

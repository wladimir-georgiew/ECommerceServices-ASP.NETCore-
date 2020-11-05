namespace FastServices.Services.Departments
{
    using System.Collections.Generic;

    using HomeServices.Data.Models;

    public interface IDepartmentsService
    {
        public List<Department> GetAllDepartments();

        public List<Department> GetAllDepartmentsWithDeleted();
    }
}

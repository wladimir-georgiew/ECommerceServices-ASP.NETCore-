namespace FastServices.Services.Departments
{
    using System.Collections.Generic;

    using FastServices.Data.Models;

    public interface IDepartmenstService
    {
        public List<Department> GetAllDepartments();

        public List<Department> GetAllDepartmentsWithDeleted();

        public IEnumerable<Service> GetDepartmentServices(int departmentId);

        public Department GetDepartment(int departmentId);
    }
}

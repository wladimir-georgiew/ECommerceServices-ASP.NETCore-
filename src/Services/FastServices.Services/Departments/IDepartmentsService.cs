namespace FastServices.Services.Departments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface IDepartmentsService
    {
        public IEnumerable<Department> GetAllDepartments();

        public IEnumerable<Department> GetAllDepartmentsWithDeleted();

        public Task<Department> GetDepartmentByIdAsync(int departmentId);

        public IEnumerable<Service> GetDepartmentServices(int departmentId);
    }
}

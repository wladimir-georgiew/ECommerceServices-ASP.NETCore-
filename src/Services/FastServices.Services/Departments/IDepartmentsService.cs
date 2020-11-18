namespace FastServices.Services.Departments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface IDepartmentsService
    {
        public IQueryable<Department> GetAllDepartments();

        public IQueryable<Department> GetAllDepartmentsWithDeleted();

        public Task<Department> GetDepartmentByIdAsync(int departmentId);

        public IEnumerable<Service> GetDepartmentServices(int departmentId);
    }
}

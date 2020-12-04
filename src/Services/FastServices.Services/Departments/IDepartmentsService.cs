using FastServices.Web.ViewModels.Administration;

namespace FastServices.Services.Departments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Departments;

    public interface IDepartmentsService
    {
        public IQueryable<Department> GetAllDepartments();

        public IQueryable<Department> GetAllDepartmentsWithDeleted();

        public Task AddAsync(Department department);

        public Task<Department> GetDepartmentByIdAsync(int departmentId);

        public IEnumerable<Service> GetDepartmentServices(int departmentId);

        public ICollection<SharedDepartmentViewModel> GetDepartmentViewModel();

        public Task AddDepartmentAsync(DepartmentInputModel model, string backgroundImgName, string cardImgName);
    }
}
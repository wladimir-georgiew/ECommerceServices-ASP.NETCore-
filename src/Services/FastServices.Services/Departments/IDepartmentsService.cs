namespace FastServices.Services.Departments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Administration;
    using FastServices.Web.ViewModels.Departments;

    public interface IDepartmentsService
    {
        public Task AddAsync(Department department);

        public IQueryable<Department> GetAllDepartments();

        public IQueryable<Department> GetAllDepartmentsWithDeleted();

        public Task<Department> GetDepartmentByIdAsync(int departmentId);

        public IEnumerable<Service> GetDepartmentServices(int departmentId);

        public ICollection<SharedDepartmentViewModel> GetDepartmentViewModel();

        public Department GetDepartmentFromModel(
            DepartmentInputModel model,
            string backgroundImgName,
            string cardImgName);

        public int GetDepartmentRatingById(int id);
    }
}
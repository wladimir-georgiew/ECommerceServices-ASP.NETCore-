using FastServices.Web.ViewModels.Administration;
using FastServices.Web.ViewModels.Departments;

namespace FastServices.Services.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Employees;

    public interface IEmployeesService
    {
        public IQueryable<Employee> GetAllWithDeleted();

        public IQueryable<Employee> GetAll();

        public IQueryable<Employee> GetDeleted();

        public Task<Employee> GetByIdWithDeletedAsync(string id);

        public Task UndeleteByIdAsync(string id);

        public Task DeleteByIdAsync(string id);

        public List<Employee> GetAllAvailableEmployees(int departmentId, DateTime startDate, DateTime dueDate);

        public Employee GetByUserId(string id);

        public Task AddEmployeeAsync(EmployeeInputModel model, ApplicationUser user);

        // public ICollection<SharedDepartmentViewModel> GetDepartmentViewModel();

        // public ApplicationUser CreateUser(EmployeeInputModel model, string uniqueFileName);
    }
}

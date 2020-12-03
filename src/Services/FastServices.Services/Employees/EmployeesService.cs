using System.IO;
using System.Security.Cryptography;
using System.Text;
using FastServices.Common;
using FastServices.Services.Departments;
using FastServices.Services.Users;
using FastServices.Web.ViewModels.Departments;

namespace FastServices.Services.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Employees;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> repository;
        private readonly IDepartmentsService departmentsService;

        public EmployeesService(
            IDeletableEntityRepository<Employee> repository,
            IDepartmentsService departmentsService)
        {
            this.repository = repository;
            this.departmentsService = departmentsService;
        }

        public async Task AddAsync(Employee employee)
        {
            await this.repository.AddAsync(employee);
        }

        public IQueryable<Employee> GetAll() => this.repository.All();

        public IQueryable<Employee> GetDeleted() => this.repository.AllWithDeleted().Where(x => x.IsDeleted == true);

        public IQueryable<Employee> GetAllWithDeleted() => this.repository.AllWithDeleted();

        public Employee GetByUserId(string id) => this.repository.All().FirstOrDefault(x => x.ApplicationUserId == id);

        public async Task<Employee> GetByIdWithDeletedAsync(string id) => await this.repository.GetByIdWithDeletedAsync(id);

        public List<Employee> GetAllAvailableEmployees(int departmentId, DateTime startDate, DateTime dueDate)
        {
            var employees = this.GetAll()
                .Where(x => x.DepartmentId == departmentId)
                .Where(x => !x.EmployeeOrders.Any(o => o.Order.StartDate <= dueDate &&
                                                       o.Order.DueDate >= startDate))
                .ToList();

            return employees;
        }

        public async Task UndeleteByIdAsync(string id)
        {
            this.repository.Undelete(await this.GetByIdWithDeletedAsync(id));
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            this.repository.Delete(await this.GetByIdWithDeletedAsync(id));
            await this.repository.SaveChangesAsync();
        }

        public async Task AddEmployeeAsync(EmployeeInputModel model, ApplicationUser user)
        {
            var employee = new Employee
            {
                Salary = model.Salary,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DepartmentId = model.DepartmentId,
                ApplicationUser = user,
                ApplicationUserId = user.Id,
            };

            await this.repository.AddAsync(employee);
            await this.repository.SaveChangesAsync();
        }
    }
}

using FastServices.Web.ViewModels.Departments;

namespace FastServices.Services.Departments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;

    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDeletableEntityRepository<Department> repository;

        public DepartmentsService(IDeletableEntityRepository<Department> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(Department department) => await this.repository.AddAsync(department);

        public IQueryable<Department> GetAllDepartments() => this.repository.All();

        public IQueryable<Department> GetAllDepartmentsWithDeleted() => this.repository.AllWithDeleted();

        public async Task<Department> GetDepartmentByIdAsync(int departmentId) => await this.repository.GetByIdWithDeletedAsync(departmentId);

        public IEnumerable<Service> GetDepartmentServices(int departmentId) => this.GetAllDepartments().Where(x => x.Id == departmentId).Select(x => x.Services).FirstOrDefault();

        public ICollection<SharedDepartmentViewModel> GetDepartmentViewModel()
        {
            var departmentsModel = this.repository.All()
                .Select(x => new SharedDepartmentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

            return departmentsModel;
        }
    }
}

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
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Department> repository;

        public DepartmentsService(IDeletableEntityRepository<Department> repository, ApplicationDbContext db)
        {
            this.db = db;
            this.repository = repository;
        }

        public IEnumerable<Department> GetAllDepartments() => this.repository.All();

        public IEnumerable<Department> GetAllDepartmentsWithDeleted() => this.repository.AllWithDeleted();

        public async Task<Department> GetDepartmentByIdAsync(int departmentId) => await this.repository.GetByIdWithDeletedAsync(departmentId);

        public IEnumerable<Service> GetDepartmentServices(int departmentId) => this.db.Departments.Where(x => x.Id == departmentId).Select(x => x.Services).FirstOrDefault();
    }
}

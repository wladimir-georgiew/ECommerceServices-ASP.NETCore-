namespace FastServices.Services.Employees
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> repository;
        private readonly ApplicationDbContext db;

        public EmployeesService(IDeletableEntityRepository<Employee> repository, ApplicationDbContext db)
        {
            this.repository = repository;
            this.db = db;
        }

        public async Task AddAsync(Employee employee)
        {
            await this.repository.AddAsync(employee);
        }

        public IEnumerable<Employee> GetAll() => this.repository.All();

        public IEnumerable<Employee> GetAvailable() => this.repository.All().ToList().Where(x => x.IsAvailable == true).ToList();

        public IEnumerable<Employee> GetDeleted() => this.repository.AllWithDeleted().Where(x => x.IsDeleted == true).ToList();

        public IEnumerable<Employee> GetAllWithDeleted() => this.repository.AllWithDeleted();

        public async Task<Employee> GetByIdAsync(string id) => await this.repository.GetByIdWithDeletedAsync(id);

        public async Task UndeleteByIdAsync(string id)
        {
            this.repository.Undelete(await this.GetByIdAsync(id));
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            this.repository.Delete(await this.GetByIdAsync(id));
            await this.db.SaveChangesAsync();
        }
    }
}

namespace FastServices.Services.Employees
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface IEmployeesService
    {
        public IQueryable<Employee> GetAllWithDeleted();

        public IQueryable<Employee> GetAll();

        public IQueryable<Employee> GetAvailable();

        public IQueryable<Employee> GetDeleted();

        public Task<Employee> GetByIdWithDeletedAsync(string id);

        public Task UndeleteByIdAsync(string id);

        public Task DeleteByIdAsync(string id);
    }
}

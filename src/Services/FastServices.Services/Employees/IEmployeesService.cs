namespace FastServices.Services.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface IEmployeesService
    {
        public IEnumerable<Employee> GetAllWithDeleted();

        public IEnumerable<Employee> GetAll();

        public IEnumerable<Employee> GetAvailable();

        public IEnumerable<Employee> GetDeleted();

        public Task<Employee> GetByIdAsync(string id);

        public Task UndeleteByIdAsync(string id);

        public Task DeleteByIdAsync(string id);
    }
}

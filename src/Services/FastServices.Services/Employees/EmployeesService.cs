namespace FastServices.Services.Employees
{
    using System.Collections.Generic;
    using System.Linq;

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

        public void AddEmployee()
        {

        }

        public IEnumerable<Employee> GetAllEmployees() => this.repository.AllWithDeleted();
    }
}

namespace FastServices.Services.Employees
{
    using System.Collections.Generic;

    using FastServices.Data.Models;

    public interface IEmployeesService
    {
        public IEnumerable<Employee> GetAllEmployees();
    }
}

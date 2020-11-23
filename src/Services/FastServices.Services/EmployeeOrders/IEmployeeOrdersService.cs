namespace FastServices.Services.EmployeeOrders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface IEmployeeOrdersService
    {
        public Task AssignEmployeesToOrderAsync(Order order, List<Employee> availableEmployees);
    }
}

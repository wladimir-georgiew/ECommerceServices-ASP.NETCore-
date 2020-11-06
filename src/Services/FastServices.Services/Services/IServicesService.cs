namespace FastServices.Services.Services
{
    using System.Collections.Generic;

    using HomeServices.Data.Models;

    public interface IServicesService
    {
        public IEnumerable<Service> GetAllServices();

        public IEnumerable<Service> GetAllServicesWithDeleted();

        public IEnumerable<Service> GetDepartmentServices(int departmentId);
    }
}

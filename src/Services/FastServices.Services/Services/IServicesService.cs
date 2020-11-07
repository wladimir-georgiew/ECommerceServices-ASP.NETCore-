namespace FastServices.Services.Services
{
    using System.Collections.Generic;

    using FastServices.Data.Models;

    public interface IServicesService
    {
        public IEnumerable<Service> GetAllServices();

        public IEnumerable<Service> GetAllServicesWithDeleted();
    }
}

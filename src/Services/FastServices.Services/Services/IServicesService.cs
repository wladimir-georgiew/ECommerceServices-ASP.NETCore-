namespace FastServices.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using FastServices.Data.Models;

    public interface IServicesService
    {
        public IQueryable<Service> GetAllServices();

        public IQueryable<Service> GetAllServicesWithDeleted();
    }
}

using FastServices.Web.ViewModels.Administration;

namespace FastServices.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

    public interface IServicesService
    {
        public IQueryable<Service> GetAllServices();

        public IQueryable<Service> GetAllServicesWithDeleted();

        public Task<Service> GetByIdWithDeletedAsync(int id);

        public Task AddAsync(Service service);

        public Task AddServiceAsync(ServiceInputModel input, string uniqueFileName);
    }
}

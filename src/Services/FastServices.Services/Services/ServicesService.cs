using FastServices.Web.ViewModels.Services;

namespace FastServices.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;

    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Service> repository;

        public ServicesService(IDeletableEntityRepository<Service> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(Service service) => await this.repository.AddAsync(service);

        public IQueryable<Service> GetAllServices() => this.repository.All();

        public IQueryable<Service> GetAllServicesWithDeleted() => this.repository.AllWithDeleted();

        public async Task<Service> GetByIdWithDeletedAsync(int id) => await this.repository.GetByIdWithDeletedAsync(id);

        public async Task AddServiceAsync(ServiceInputModel input, string uniqueFileName)
        {
            var service = new Service
            {
                Name = input.Name,
                DepartmentId = input.DepartmentId,
                Fee = input.Fee,
                Description = input.Description,
                CardImgSrc = !string.IsNullOrEmpty(uniqueFileName)
                    ? ("/images/" + uniqueFileName)
                    : "/defaultImages/defBackgroundImg.png",
            };

            await this.AddAsync(service);
            await this.repository.SaveChangesAsync();
        }
    }
}

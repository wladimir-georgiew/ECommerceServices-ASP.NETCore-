namespace FastServices.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;

    public class ServicesService : IServicesService
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Service> repository;

        public ServicesService(ApplicationDbContext db, IDeletableEntityRepository<Service> repository)
        {
            this.db = db;
            this.repository = repository;
        }

        public IEnumerable<Service> GetAllServices() => this.repository.All();

        public IEnumerable<Service> GetAllServicesWithDeleted() => this.repository.AllWithDeleted();
    }
}

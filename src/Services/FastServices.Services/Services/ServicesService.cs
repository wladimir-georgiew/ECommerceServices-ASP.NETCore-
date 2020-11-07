namespace FastServices.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using FastServices.Data;
    using FastServices.Data.Models;

    public class ServicesService : IServicesService
    {
        private readonly ApplicationDbContext db;

        public ServicesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Service> GetAllServices() => this.db.Services.Where(x => x.IsDeleted == false).ToList();

        public IEnumerable<Service> GetAllServicesWithDeleted() => this.db.Services.ToList();
    }
}

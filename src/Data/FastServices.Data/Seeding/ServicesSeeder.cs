namespace FastServices.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HomeServices.Data.Models;

    public class ServicesSeeder : ISeeder
    {
        private ApplicationDbContext db;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.db = dbContext;

            Dictionary<string, string[]> servicesDictionary = new Dictionary<string, string[]>();

            string[] cleaning = { "Home Cleaning", "Office Cleaning", };
            string[] assembly = { "Small-Medium Furniture Assembly", "Big Furniture Assembly" };
            string[] plumbing = { "Toilet Plumbing", "Drain Repair" };
            string[] electrical = { "Outlet Installation", "Light Fixtures", "Smart Thermostat", "Security Cam Installation", "Light Switch Installation" };
            string[] painting = { "Interior Painting", "Exterior Painting" };
            string[] moving = { "Moving Help" };

            servicesDictionary.Add("Cleaning", cleaning);
            servicesDictionary.Add("Assembly", assembly);
            servicesDictionary.Add("Plumbing", plumbing);
            servicesDictionary.Add("Electrical", electrical);
            servicesDictionary.Add("Painting", painting);
            servicesDictionary.Add("Moving", moving);

            await this.SeedServicesAsync(servicesDictionary);
        }

        private async Task SeedServicesAsync(Dictionary<string, string[]> servicesDictionary)
        {
            List<Service> services = new List<Service>();

            foreach (var kvp in servicesDictionary)
            {
                int depId = this.db.Departments.FirstOrDefault(x => x.Name == kvp.Key).Id;

                foreach (string sname in kvp.Value)
                {
                    Service newService = new Service
                    {
                        DepartmentId = depId,
                        Name = sname,
                        Fee = 10M,
                    };

                    if (!this.db.Services.Any(x => x.Name == newService.Name))
                    {
                        services.Add(newService);
                    }
                }
            }

            await this.db.AddRangeAsync(services);
        }
    }
}

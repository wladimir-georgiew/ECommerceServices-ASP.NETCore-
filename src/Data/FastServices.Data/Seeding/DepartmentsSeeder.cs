namespace FastServices.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HomeServices.Data.Models;

    public class DepartmentsSeeder : ISeeder
    {
        private ApplicationDbContext db;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.db = dbContext;
            string[] departmentNames = { "Plumbing", "Electrical", "Painting", "Moving", "Cleaning", "Assembly" };

            await this.SeedDepartmentsAsync(departmentNames);
        }

        private async Task SeedDepartmentsAsync(string[] departmentNames)
        {
            List<Department> departments = new List<Department>();

            foreach (string name in departmentNames)
            {
                Department department = new Department { Name = name };

                if (!this.db.Departments.Any(x => x.Name == department.Name))
                {
                    departments.Add(department);
                }
            }

            await this.db.AddRangeAsync(departments);
        }
    }
}

namespace FastServices.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

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
                if (!this.db.Departments.Any(x => x.Name == name))
                {
                    Department department = new Department();

                    if (name == "Plumbing")
                    {
                        department.Name = name;
                        department.Description = "Installing pipes and fixtures, such as sinks and toilets, for water, gas, steam, air, or other liquids.";
                        department.CardImgSrc = "/Template/images/bb2.jpg";
                        department.BackgroundImgSrc = "/Template/images/depBgPlumbing1.jpg";
                    }
                    else if (name == "Electrical")
                    {
                        department.Name = name;
                        department.Description = "Inspecting, testing, repairing, installing, and modifying electrical components and systems";
                        department.CardImgSrc = "/Template/images/bb3.jpg";
                        department.BackgroundImgSrc = "/Template/images/depBgElectrical1.jpg";
                    }
                    else if (name == "Painting")
                    {
                        department.Name = name;
                        department.Description = "Preparing walls and other surfaces for painting by using sandpaper, scraping and removing old paint";
                        department.CardImgSrc = "/Template/images/bb5.jpg";
                        department.BackgroundImgSrc = "/Template/images/depBgPainting1.jpg";
                    }
                    else if (name == "Moving")
                    {
                        department.Name = name;
                        department.Description = "Moving help, packing boxes, unpacking boxes, heavy lifting, and loading items into a vehicle";
                        department.CardImgSrc = "https://manvankingston.co.uk/wp-content/uploads/2019/12/Furniture-Moving.png";
                        department.BackgroundImgSrc = "/Template/images/depBgMoving1.jpg";
                    }
                    else if (name == "Cleaning")
                    {
                        department.Name = name;
                        department.Description = "Dusting, mopping, sweeping, waxing floors and vacuuming.";
                        department.CardImgSrc = "https://www.pioneercleaningsystemsinc.com/wp-content/uploads/2019/11/services-img.jpg";
                        department.BackgroundImgSrc = "/Template/images/depBgCleaning1.jpg";
                    }
                    else if (name == "Assembly")
                    {
                        department.Name = name;
                        department.Description = "Assembling small, medium and big furnitures";
                        department.CardImgSrc = "https://www.myjobquote.co.uk/assets/img/flat-pack-furniture-assembly-cost-1.jpg";
                        department.BackgroundImgSrc = "/Template/images/depBgAssembly1.jpg";
                    }

                    departments.Add(department);
                }
            }

            await this.db.AddRangeAsync(departments);
        }
    }
}

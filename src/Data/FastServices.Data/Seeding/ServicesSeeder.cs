namespace FastServices.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FastServices.Data.Models;

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
            string[] electrical = { "Outlet Installation", "Light Fixtures", "Smart Thermostat", "Security Cam Installation" };
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
                Department department = this.db.Departments.FirstOrDefault(x => x.Name == kvp.Key);

                foreach (string sname in kvp.Value)
                {
                    if (!this.db.Services.Any(x => x.Name == sname))
                    {
                        Service newService = new Service();

                        newService.Name = sname;
                        newService.DepartmentId = department.Id;

                        // Cleaning Services
                        if (kvp.Key == "Cleaning")
                        {
                            if (sname == "Home Cleaning")
                            {
                                newService.Fee = 10;
                                newService.CardImgSrc = "https://www.bestchoice.bg/var/tinymce/upload/Home%20Cleaning.jpg";
                            }
                            else if (sname == "Office Cleaning")
                            {
                                newService.Fee = 15;
                                newService.CardImgSrc = "https://xceljanitorial.com/wp-content/uploads/2019/01/5bed093a9a01db72fc42e81d_office-cleaning-10-min-300x200.jpg";
                            }
                        }

                        // Assembly Services
                        else if (kvp.Key == "Assembly")
                        {
                            if (sname == "Small-Medium Furniture Assembly")
                            {
                                newService.Fee = 8;
                                newService.CardImgSrc = "https://www.godownsize.com/wp-content/uploads/2013/09/furniture-to-small-spaces.jpg";
                            }
                            else if (sname == "Big Furniture Assembly")
                            {
                                newService.Fee = 13;
                                newService.CardImgSrc = "https://www.hammonds-uk.com/sites/default/files/2018-05/Willoughby%20Overbed%20Storage%201.jpg";
                            }
                        }

                        // Plumbing Services
                        else if (kvp.Key == "Plumbing")
                        {
                            if (sname == "Toilet Plumbing")
                            {
                                newService.Fee = 18;
                                newService.CardImgSrc = "https://fastplumbers.net.au/wp-content/uploads/2019/05/plumber-repairing-a-clogged-toilet-with-a-plunger.jpg";
                            }
                            else if (sname == "Drain Repair")
                            {
                                newService.Fee = 20;
                                newService.CardImgSrc = "https://reckonplumbing.com/wp-content/uploads/2012/06/drain-repair-los-angeles-300x180.jpg";
                            }
                        }

                        // Electrical Services
                        else if (kvp.Key == "Electrical")
                        {
                            if (sname == "Outlet Installation")
                            {
                                newService.Fee = 5;
                                newService.CardImgSrc = "https://cdn.fixr.com/cost_guide_pictures/install-electrical-outlet-5ea98bb810c91.png";
                            }
                            else if (sname == "Light Fixtures")
                            {
                                newService.Fee = 5;
                                newService.CardImgSrc = "https://www.excelelectrical.net/wp-content/uploads/2017/11/electricians-in-stafford.jpg";
                            }
                            else if (sname == "Smart Thermostat")
                            {
                                newService.Fee = 60;
                                newService.CardImgSrc = "https://www.daddy-geek.com/wp-content/uploads/2019/04/thermostat.jpg";
                            }
                            else if (sname == "Security Cam Installation")
                            {
                                newService.Fee = 50;
                                newService.CardImgSrc = "https://randymatheson.com/wp-content/uploads/2012/06/coca_cola_security_cam_ad.jpg";
                            }
                        }

                        // Painting Services
                        else if (kvp.Key == "Painting")
                        {
                            if (sname == "Interior Painting")
                            {
                                newService.Fee = 35;
                                newService.CardImgSrc = "https://certapro.ca/wp-content/uploads/sites/1482/cache//Screen-Shot-2018-04-02-at-10_49_06-PM/2047455819.png";
                            }
                            else if (sname == "Exterior Painting")
                            {
                                newService.Fee = 50;
                                newService.CardImgSrc = "https://1lkjxqh1ny42ecbi51yqhrm4-wpengine.netdna-ssl.com/wp-content/uploads/cache//featured-image-1/58357269.jpg";
                            }
                        }

                        // Moving Services
                        else if (kvp.Key == "Moving")
                        {
                            if (sname == "Moving Help")
                            {
                                newService.Fee = 20;
                                newService.CardImgSrc = "https://www.uhaul.com/Blog/wp-content/uploads/2015/11/moving-help.png";
                            }
                        }

                        department.Services.Add(newService);
                        services.Add(newService);
                    }
                }
            }

            await this.db.AddRangeAsync(services);
        }
    }
}

namespace FastServices.Web.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using FastServices.Data;
    using HomeServices.Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentsPopUpViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public DepartmentsPopUpViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke(string depName)
        {
            var department = this.db.Departments.FirstOrDefault(x => x.Name == depName);

            var model = new DepartmentPopUpViewModel
            {
                Name = depName,
                Services = this.db.Services.Where(x => x.DepartmentId == department.Id).ToList(),
            };

            return this.View(model);
        }
    }
}

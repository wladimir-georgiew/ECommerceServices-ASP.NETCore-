namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Services.Departments;
    using FastServices.Web.ViewModels.Departments;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ApplicationDbContext db;

        public DepartmentsController(IDepartmentsService departmentsService, ApplicationDbContext db)
        {
            this.departmentsService = departmentsService;
            this.db = db;
        }

        public IActionResult Department(int id)
        {
            string bgUrl = this.db.Departments.FirstOrDefault(x => x.Id == id).BackgroundImgSrc;
            string depName = this.db.Departments.FirstOrDefault(x => x.Id == id).Name;

            List<ServicesViewModel> servicesViewModel = this.departmentsService.GetDepartmentServices(id)
                .Select(x => new ServicesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CardImgSrc = x.CardImgSrc,
                })
                .ToList();

            this.ViewData["url"] = bgUrl;
            this.ViewData["depName"] = depName.ToUpper();

            var model = new Tuple<
                ICollection<ServicesViewModel>, OrderModalViewModel
                >(servicesViewModel, new OrderModalViewModel());

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Department(OrderModalViewModel model)
        {
            // add a custom error
            this.ModelState.AddModelError(string.Empty, "This is a custom error for testing!");

            // check the model (you should also do front-end vlidation, as in the demo)
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // do your stuff


            // add a success user message
            model.ResultMessage = "Your form has been submitted.";

            return this.View(model);
        }
    }
}

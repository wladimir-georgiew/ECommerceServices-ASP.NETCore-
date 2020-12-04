using FastServices.Web.ViewModels.Administration;

namespace FastServices.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FastServices.Common;
    using FastServices.Services.Departments;
    using FastServices.Services.Images;
    using FastServices.Web.ViewModels.Departments;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentsController : AdministrationController
    {
        private readonly IImageServices imageServices;
        private readonly IDepartmentsService departmentsService;

        public DepartmentsController(
           IImageServices imageServices,
           IDepartmentsService departmentsService)
        {
            this.imageServices = imageServices;
            this.departmentsService = departmentsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new DepartmentInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentInputModel model)
        {
            if (this.departmentsService.GetAllDepartments()
                .Any(x => x.Name == model.Name))
            {
                #pragma warning disable SA1122
                this.ModelState.AddModelError("", "Department with such name already exists");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string backgroundImgName = this.imageServices.GetUploadedFileName(model.BackgroundImage);
            string cardImgName = this.imageServices.GetUploadedFileName(model.CardImage);

            await this.departmentsService.AddDepartmentAsync(model, backgroundImgName, cardImgName);

            this.TempData["msg"] = GlobalConstants.SuccessAddDepartment;
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}

using FastServices.Web.ViewModels.Administration;

namespace FastServices.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Services.Departments;
    using FastServices.Services.Images;
    using FastServices.Services.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class ServicesController : AdministrationController
    {
        private readonly IDepartmentsService departmentsService;
        private readonly IServicesService servicesService;
        private readonly IImageServices imageServices;

        public ServicesController(
            IDepartmentsService departmentsService,
            IServicesService servicesService,
            IImageServices imageServices)
        {
            this.departmentsService = departmentsService;
            this.servicesService = servicesService;
            this.imageServices = imageServices;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateServiceMasterModel
            {
                Departments = this.departmentsService.GetDepartmentViewModel(),
                InputModel = new ServiceInputModel(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceMasterModel model)
        {
            var departmentsModel = this.departmentsService.GetDepartmentViewModel();

            var masterModel = new CreateServiceMasterModel
            {
                Departments = departmentsModel,
                InputModel = model.InputModel,
            };

            if (this.servicesService.GetAllServices()
                .Any(x => x.Name == model.InputModel.Name))
            {
                #pragma warning disable SA1122
                this.ModelState.AddModelError("", "Service with such name already exists");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(masterModel);
            }

            string uniqueFileName = this.imageServices.GetUploadedFileName(model.InputModel.BackgroundImage);
            await this.servicesService.AddServiceAsync(model.InputModel, uniqueFileName);

            this.TempData["msg"] = GlobalConstants.SuccessAddService;
            return this.RedirectToAction("Department", "Departments", new { area = "", depId = model.InputModel.DepartmentId });
        }
    }
}

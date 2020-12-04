using FastServices.Services.Images;
using FastServices.Web.ViewModels.Administration;

namespace FastServices.Web.Areas.Administration.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Data.Models;
    using FastServices.Services.Departments;
    using FastServices.Services.Employees;
    using FastServices.Services.Users;
    using FastServices.Web.ViewModels.Employees;
    using FastServices.Web.ViewModels.PaginationList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class EmployeesController : AdministrationController
    {
        private const int EmployeesPerPage = 10;

        private readonly IEmployeesService employeesService;
        private readonly IDepartmentsService departmentsService;
        private readonly IImageServices imageServices;
        private readonly IUsersService usersService;

        public EmployeesController(
            IEmployeesService employeesService,
            IDepartmentsService departmentsService,
            IImageServices imageServices,
            IUsersService usersService)
        {
            this.employeesService = employeesService;
            this.departmentsService = departmentsService;
            this.imageServices = imageServices;
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Employees(
            int selectedOption,
            string id,
            string searchString,
            int pageNumber = 1)
        {
            var employees = this.employeesService.GetAllWithDeleted().ToList();

            if (selectedOption == 2)
            {
                employees = this.employeesService.GetDeleted().ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.LastName.ToLower().Contains(searchString.ToLower())
                                       || e.FirstName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            this.TempData["SelectedOption"] = selectedOption;

            var model = employees
                .Select(x => new EmployeeViewModel
                {
                    Id = x.Id,
                    DepartmentName = this.departmentsService.GetDepartmentByIdAsync(x.DepartmentId).GetAwaiter().GetResult().Name,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Salary = x.Salary,
                    IsDeleted = x.IsDeleted,
                    DeletedOn = x.DeletedOn?.ToString(format: "d"),
                    CreatedOn = x.CreatedOn.ToString(format: "d"),
                    HtmlClass = x.IsDeleted == true ? "table-danger" : "table-success",
                    AvatarImgSrc = this.usersService.GetByIdWithDeletedAsync(x.ApplicationUserId).GetAwaiter().GetResult().AvatarImgSrc,
                })
                .ToList()
                .OrderBy(x => x.DepartmentName)
                .ThenByDescending(x => x.CreatedOn)
                .ThenBy(x => x.FirstName)
                .ToList();

            var paginatedModel = PaginationList<EmployeeViewModel>.Create(model, pageNumber, EmployeesPerPage);

            return this.View(paginatedModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateEmployeeMasterModel
            {
                Departments = this.departmentsService.GetDepartmentViewModel(),
                InputModel = new EmployeeInputModel(),
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeMasterModel model)
        {
            var departmentsModel = this.departmentsService.GetDepartmentViewModel();

            var masterModel = new CreateEmployeeMasterModel
            {
                Departments = departmentsModel,
                InputModel = model.InputModel,
            };

            if (model.InputModel.Password != model.InputModel.ConfirmPassword)
            {
                #pragma warning disable SA1122
                this.ModelState.AddModelError("", "Passwords don't match!");
            }

            if (this.usersService.GetAll().Any(x => x.Email == model.InputModel.Email))
            {
                this.ModelState.AddModelError("Email", "Email already taken");
            }

            if (this.usersService.GetAll().Any(x => x.PhoneNumber == model.InputModel.PhoneNumber))
            {
                this.ModelState.AddModelError("PhoneNumber", "Phone number already taken");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(masterModel);
            }

            string uniqueFileName = this.imageServices.GetUploadedFileName(model.InputModel.ProfileImage);

            var user = await this.usersService.CreateUserAsync(model.InputModel, uniqueFileName);

            await this.employeesService.AddEmployeeAsync(model.InputModel, user);

            await this.usersService.AssignUserToRoleAsync(GlobalConstants.EmployeeRoleName, user);

            this.TempData["msg"] = GlobalConstants.SuccessAddEmployee;
            return this.RedirectToAction(nameof(this.Employees));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployees(int selectedOption, string id)
        {
            await this.employeesService.DeleteByIdAsync(id);

            return this.Redirect($"/Administration/Employees/Employees?selectedOption={selectedOption}");
        }

        [HttpPost]
        public async Task<IActionResult> UndeleteEmployees(int selectedOption, string id)
        {
            await this.employeesService.UndeleteByIdAsync(id);

            return this.Redirect($"/Administration/Employees/Employees?selectedOption={selectedOption}");
        }
    }
}

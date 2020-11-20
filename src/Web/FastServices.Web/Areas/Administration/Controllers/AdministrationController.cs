namespace FastServices.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Data.Models;
    using FastServices.Services.Departments;
    using FastServices.Services.Employees;
    using FastServices.Web.Controllers;
    using FastServices.Web.ViewModels.Employees;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IEmployeesService employeesService;
        private readonly IDepartmentsService departmentService;

        public AdministrationController(IEmployeesService employeesService, IDepartmentsService departmentService)
        {
            this.employeesService = employeesService;
            this.departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Employees(int selectedOption, string id, string searchString)
        {
            var employees = this.employeesService.GetAllWithDeleted().ToList();

            if (selectedOption == 2)
            {
                employees = this.employeesService.GetAvailable().ToList();
            }
            else if (selectedOption == 3)
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
                    DepartmentName = this.departmentService.GetDepartmentByIdAsync(x.DepartmentId).GetAwaiter().GetResult().Name,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsAvailable = (x.IsAvailable == true && x.IsDeleted == false) ? "Yes" : "No",
                    Salary = x.Salary,
                    IsDeleted = x.IsDeleted,
                    DeletedOn = x.DeletedOn?.ToString(format: "d"),
                    CreatedOn = x.CreatedOn.ToString(format: "d"),
                    HtmlClass = x.IsDeleted == true ? "table-danger"
                                : x.IsAvailable == false ? "table-warning"
                                : "table-success",
                })
                .ToList();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Employees(string operationToExec, int selectedOption, string id, string searchString)
        {
            if (operationToExec == "Restore")
            {
                await this.employeesService.UndeleteByIdAsync(id);
            }
            else if (operationToExec == "Delete")
            {
                await this.employeesService.DeleteByIdAsync(id);
            }

            return this.Redirect($"/Administration/Administration/Employees?selectedOption={selectedOption}");

        }
    }
}

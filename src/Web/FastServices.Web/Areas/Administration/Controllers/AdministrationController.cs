namespace FastServices.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Data.Models;
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

        public AdministrationController(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        [HttpGet]
        public async Task<IActionResult> Employees(int selectedOption, string make, string id)
        {
            if (!string.IsNullOrEmpty(make))
            {
                if (make == "restore")
                {
                    await this.employeesService.UndeleteByIdAsync(id);
                }
                else if (make == "delete")
                {
                    await this.employeesService.DeleteByIdAsync(id);
                }

                return this.Redirect($"/Administration/Administration/Employees?selectedOption={selectedOption}");
            }

            var employees = this.employeesService.GetAllWithDeleted().ToList();

            if (selectedOption == 2)
            {
                employees = this.employeesService.GetAvailable().ToList();
            }
            else if (selectedOption == 3)
            {
                employees = this.employeesService.GetDeleted().ToList();
            }

            this.TempData["SelectedOption"] = selectedOption;

            var model = employees
                .Select(x => new EmployeeViewModel
                {
                    Id = x.Id,
                    UserId = x.ApplicationUserId,
                    DepartmentId = x.DepartmentId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsAvailable = x.IsAvailable,
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
    }
}

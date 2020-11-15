namespace FastServices.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

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
        public IActionResult Employees()
        {
            string htmlClass = string.Empty;

            var model = this.employeesService.GetAllEmployees()
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
                    HtmlClass = x.IsAvailable == false ? "table-warning"
                                : x.IsDeleted == true ? "table-danger"
                                : string.Empty,
                })
                .ToList();

            return this.View(model);
        }
    }
}

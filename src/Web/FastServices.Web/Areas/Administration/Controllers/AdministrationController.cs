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
    }
}

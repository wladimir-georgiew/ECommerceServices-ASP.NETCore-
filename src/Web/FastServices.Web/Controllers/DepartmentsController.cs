namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class DepartmentsController : Controller
    {
        public IActionResult Department()
        {
            return this.View();
        }
    }
}

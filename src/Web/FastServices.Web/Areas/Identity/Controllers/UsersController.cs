using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FastServices.Web.Areas.Identity.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet("/Identity/Users/Login")]
        public IActionResult Login()
        {
            return this.View();
        }
    }
}

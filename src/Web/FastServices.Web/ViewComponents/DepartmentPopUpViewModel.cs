namespace FastServices.Web.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using FastServices.Data;
    using HomeServices.Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentPopUpViewModel
    {
        public string Name { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}

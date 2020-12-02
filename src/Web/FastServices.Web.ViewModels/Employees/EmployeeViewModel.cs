namespace FastServices.Web.ViewModels.Employees
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class EmployeeViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DepartmentName { get; set; }

        public decimal Salary { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedOn { get; set; }

        public string CreatedOn { get; set; }

        public string HtmlClass { get; set; }

        public string AvatarImgSrc { get; set; }
    }
}

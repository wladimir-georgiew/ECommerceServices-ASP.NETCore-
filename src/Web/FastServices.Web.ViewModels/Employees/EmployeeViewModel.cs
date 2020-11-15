namespace FastServices.Web.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }

        public int Department { get; set; }

        public bool IsAvailable { get; set; }

        public decimal Salary { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedOn { get; set; }

        public string CreatedOn { get; set; }

        public string HtmlClass { get; set; }
    }
}

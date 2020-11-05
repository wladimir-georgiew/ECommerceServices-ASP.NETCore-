namespace FastServices.Services.Departments
{
    using System.Collections.Generic;
    using System.Linq;
    using FastServices.Data;
    using HomeServices.Data.Models;

    public class DepartmentsService : IDepartmentsService
    {
        private readonly ApplicationDbContext db;

        public DepartmentsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Department> GetAllDepartments() => this.db.Departments.Where(x => x.IsDeleted == false).ToList();

        public List<Department> GetAllDepartmentsWithDeleted() => this.db.Departments.ToList();
    }
}

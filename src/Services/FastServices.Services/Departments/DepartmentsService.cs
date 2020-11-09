﻿namespace FastServices.Services.Departments
{
    using System.Collections.Generic;
    using System.Linq;

    using FastServices.Data;
    using FastServices.Data.Models;

    public class DepartmentsService : IDepartmentsService
    {
        private readonly ApplicationDbContext db;

        public DepartmentsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Department> GetAllDepartments() => this.db.Departments.Where(x => x.IsDeleted == false).ToList();

        public List<Department> GetAllDepartmentsWithDeleted() => this.db.Departments.ToList();

        // public IEnumerable<Service> GetDepartmentServices(int departmentId) => this.db.Services.Where(x => x.DepartmentId == departmentId).ToList();

        public IEnumerable<Service> GetDepartmentServices(int departmentId) => this.db.Departments.Where(x => x.Id == departmentId).Select(x => x.Services).FirstOrDefault();
    }
}
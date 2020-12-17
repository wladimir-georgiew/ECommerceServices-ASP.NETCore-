using FastServices.Web.ViewModels.Administration;
using FastServices.Web.ViewModels.Departments;

namespace FastServices.Services.Departments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Services.Images;
    using Microsoft.AspNetCore.Http;

    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDeletableEntityRepository<Department> repository;
        private readonly IImageServices imagesService;

        public DepartmentsService(
            IDeletableEntityRepository<Department> repository,
            IImageServices imagesService)
        {
            this.repository = repository;
            this.imagesService = imagesService;
        }

        public async Task AddAsync(Department department)
        {
            await this.repository.AddAsync(department);
            await this.repository.SaveChangesAsync();
        }

        public IQueryable<Department> GetAllDepartments() => this.repository.All();

        public IQueryable<Department> GetAllDepartmentsWithDeleted() => this.repository.AllWithDeleted();

        public async Task<Department> GetDepartmentByIdAsync(int departmentId) => await this.repository.GetByIdWithDeletedAsync(departmentId);

        public IEnumerable<Service> GetDepartmentServices(int departmentId) => this.GetAllDepartments().Where(x => x.Id == departmentId).Select(x => x.Services).FirstOrDefault();

        public ICollection<SharedDepartmentViewModel> GetDepartmentViewModel()
        {
            var departmentsModel = this.repository.All()
                .Select(x => new SharedDepartmentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

            return departmentsModel;
        }

        public Department GetDepartmentFromModel(DepartmentInputModel model, string backgroundImgName, string cardImgName)
        {
            var department = new Department
            {
                Name = model.Name,
                Description = model.Description,
                BackgroundImgSrc = "/images/" + backgroundImgName,
                CardImgSrc = !string.IsNullOrEmpty(cardImgName)
                    ? ("/images/" + cardImgName)
                    : "/defaultImages/defBackgroundImg.png",
            };

            return department;
        }

        public int GetDepartmentRatingById(int id)
        {
            var departmentStars = this.GetAllDepartments()
                .Where(x => x.Id == id)
                .SelectMany(x => x.Comments)
                .Select(x => x.Stars);

            var result = (int)Math.Ceiling((double)departmentStars.Sum() / departmentStars.Count());

            return result;
        }
    }
}

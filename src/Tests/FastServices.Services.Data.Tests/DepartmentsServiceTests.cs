namespace FastServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Data.Repositories;
    using FastServices.Services.Comments;
    using FastServices.Services.Departments;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class DepartmentsServiceTests
    {
        private List<Department> list;
        private Mock<IDeletableEntityRepository<Department>> repository;
        private IWebHostEnvironment dep1;

        public DepartmentsServiceTests()
        {
            this.list = new List<Department>();
            this.repository = new Mock<IDeletableEntityRepository<Department>>();
        }

        [Fact]
        public async Task AddAsyncShouldAddCorrectly()
        {
            this.repository.Setup(r => r.All())
                .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .Callback((Department department) => this.list.Add(department));

            var service = new DepartmentsService(
                this.repository.Object,
                new ImageServices(this.dep1));

            await service.AddAsync(new Department
            {
                Id = 1,
            });

            var department = this.list.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(department);
        }

        [Fact]
        public async Task RepositoryCountShouldReturnCorrectValue()
        {
            this.repository.Setup(r => r.All())
                .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .Callback((Department department) => this.list.Add(department));

            var service = new DepartmentsService(
                this.repository.Object,
                new ImageServices(this.dep1));

            await service.AddAsync(new Department());
            await service.AddAsync(new Department());
            await service.AddAsync(new Department());

            Assert.Equal(3, this.list.Count());
        }

        [Fact]
        public async Task GetAllDepartmentsShouldReturnAllDepartmentsWithoutDeleted()
        {
            this.repository.Setup(r => r.All())
                .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .Callback((Department department) => this.list.Add(department));

            var service = new DepartmentsService(
                this.repository.Object,
                new ImageServices(this.dep1));

            await service.AddAsync(new Department());
            await service.AddAsync(new Department());
            await service.AddAsync(new Department());
            await service.AddAsync(new Department { IsDeleted = true });
            await service.AddAsync(new Department { IsDeleted = true });

            var allDepartments = service.GetAllDepartments();

            Assert.Equal(3, allDepartments.Count());
        }

        [Fact]
        public async Task GetAllDepartmentsWithDeletedShouldReturnAllDepartmentsWithDeleted()
        {
            this.repository.Setup(r => r.AllWithDeleted())
                .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .Callback((Department department) => this.list.Add(department));

            var service = new DepartmentsService(
                this.repository.Object,
                new ImageServices(this.dep1));

            await service.AddAsync(new Department());
            await service.AddAsync(new Department());
            await service.AddAsync(new Department());
            await service.AddAsync(new Department { IsDeleted = true });
            await service.AddAsync(new Department { IsDeleted = true });

            var allDepartments = service.GetAllDepartmentsWithDeleted();

            Assert.Equal(5, allDepartments.Count());
        }

        [Fact]
        public async Task GetDepartmentRatingByIdShouldWorkProperly()
        {
            this.repository.Setup(r => r.All())
                .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .Callback((Department department) => this.list.Add(department));

            var service = new DepartmentsService(
                this.repository.Object,
                new ImageServices(this.dep1));

            await service.AddAsync(new Department
            {
                Id = 5,
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Stars = 5,
                    },
                    new Comment
                    {
                        Stars = 3,
                    },
                },
            });

            var departmentStars = service.GetAllDepartments().Where(x => x.Id == 5).SelectMany(x => x.Comments).Select(x => x.Stars);

            var result = (int)Math.Ceiling((double)departmentStars.Sum() / departmentStars.Count());

            Assert.Equal(service.GetDepartmentRatingById(5), result);
        }

        [Fact]
        public async Task GetDepartmentServicesShouldWorkProperly()
        {
            this.repository.Setup(r => r.All())
                .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .Callback((Department department) => this.list.Add(department));

            var service = new DepartmentsService(
                this.repository.Object,
                new ImageServices(this.dep1));

            await service.AddAsync(new Department { Id = 5, Services = new List<Service> { new Service() } });

            Assert.Single(service.GetDepartmentServices(5));
        }

        //[Fact]
        //public async Task GetDepartmentByIdAsyncShouldReturnDepartmentIfItExists()
        //{
        //    this.repository.Setup(r => r.All())
        //        .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

        //    this.repository.Setup(r => r.GetByIdWithDeletedAsync(It.IsAny<long>()))
        //        .Returns(async (int id) => this.list.Single(x => x.Id == id));

        //    this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
        //        .Callback((Department department) => this.list.Add(department));

        //    var service = new DepartmentsService(
        //        this.repository.Object,
        //        new ImageServices(this.dep1));

        //    await service.AddAsync(new Department { Id = 5, IsDeleted = true, });

        //    var department = await service.GetDepartmentByIdAsync(5);

        //    Assert.NotNull(department);
        //}

        //[Fact]
        //public async Task GetDepartmentByIdAsyncShouldReturnNullIfTheGivenIdDoesNotExist()
        //{
        //    this.repository.Setup(r => r.All())
        //        .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

        //    this.repository.Setup(r => r.AddAsync(It.IsAny<Department>()))
        //        .Callback((Department department) => this.list.Add(department));

        //    var service = new DepartmentsService(
        //        this.repository.Object,
        //        new ImageServices(this.dep1));

        //    await service.AddAsync(new Department { Id = 5 });

        //    var department = await service.GetDepartmentByIdAsync(185657);

        //    Assert.Null(department);
        //}
    }
}

namespace FastServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Services.Employees;
    using Moq;
    using Xunit;

    public class EmployeesServiceTests
    {
        private List<Employee> list;
        private Mock<IDeletableEntityRepository<Employee>> repository;

        public EmployeesServiceTests()
        {
            this.list = new List<Employee>();
            this.repository = new Mock<IDeletableEntityRepository<Employee>>();
        }

        [Fact]
        public async Task GetDeletedShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.AllWithDeleted())
              .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == true));

            this.repository.Setup(r => r.AddAsync(It.IsAny<Employee>()))
               .Callback((Employee employee) => this.list.Add(employee));

            var service = new EmployeesService(this.repository.Object);

            var employeeFake = new Employee
            {
                IsDeleted = true,
            };

            await service.AddAsync(employeeFake);

            var collection = service.GetDeleted().ToList();

            Assert.Single(collection);
        }

        [Fact]
        public void GetAllWithDeletedShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.AllWithDeleted())
              .Returns(this.list.AsQueryable());

            var service = new EmployeesService(this.repository.Object);

            var employeeDeleted = new Employee
            {
                IsDeleted = true,
            };
            var employeeNotDeleted = new Employee
            {
                IsDeleted = false,
            };

            this.list.Add(employeeDeleted);
            this.list.Add(employeeNotDeleted);

            var employees = service.GetAllWithDeleted();

            Assert.Equal(2, employees.Count());
        }

        [Fact]
        public void GetAllAvailableEmployeesShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            var service = new EmployeesService(this.repository.Object);

            var employeeNotAvailableFake = new Employee
            {
                Id = "1",
                DepartmentId = 5,
                EmployeeOrders = new List<EmployeeOrder>
                {
                    new EmployeeOrder { Order = new Order { StartDate = DateTime.UtcNow.AddHours(2), DueDate = DateTime.UtcNow.AddHours(5) } },
                },
            };

            this.list.Add(employeeNotAvailableFake);

            var employeeAvailableFake = new Employee
            {
                Id = "2",
                DepartmentId = 5,
                EmployeeOrders = new List<EmployeeOrder>
                {
                    new EmployeeOrder { Order = new Order { StartDate = DateTime.UtcNow.AddDays(10), DueDate = DateTime.UtcNow.AddDays(11) } },
                },
            };

            this.list.Add(employeeAvailableFake);

            var employees = service.GetAllAvailableEmployees(5, DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

            Assert.Single(employees);
        }

        [Fact]
        public void GetByUserIdShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            var service = new EmployeesService(this.repository.Object);

            var fakeEmployee = new Employee
            {
                ApplicationUserId = "userid1",
            };
            var fakeEmployee2 = new Employee
            {
                ApplicationUserId = "userid2",
            };

            this.list.Add(fakeEmployee);
            this.list.Add(fakeEmployee2);

            var employee = service.GetByUserId("userid1");

            Assert.NotNull(employee);
            Assert.Equal("userid1", employee.ApplicationUserId);
        }
    }
}

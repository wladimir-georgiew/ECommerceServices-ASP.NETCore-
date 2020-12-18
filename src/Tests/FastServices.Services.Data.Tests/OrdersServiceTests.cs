namespace FastServices.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Services.EmployeeOrders;
    using FastServices.Services.Employees;
    using FastServices.Services.Orders;
    using FastServices.Web.ViewModels.Orders;
    using Moq;
    using Xunit;

    public class OrdersServiceTests
    {
        private List<Order> list;
        private Mock<IRepository<Order>> repository;
        private Mock<IRepository<EmployeeOrder>> repositoryEmployeesOrders;
        private Mock<IDeletableEntityRepository<Employee>> repositoryEmployees;

        public OrdersServiceTests()
        {
            this.list = new List<Order>();
            this.repository = new Mock<IRepository<Order>>();
            this.repositoryEmployeesOrders = new Mock<IRepository<EmployeeOrder>>();
            this.repositoryEmployees = new Mock<IDeletableEntityRepository<Employee>>();
        }

        [Fact]
        public void GetAllShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Order>()))
               .Callback((Order order) => this.list.Add(order));

            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            this.list.Add(new Order());

            var orders = service.GetAll();

            Assert.NotNull(orders);
            Assert.Single(orders);
        }

        [Fact]
        public void GetByIdShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Order>()))
               .Callback((Order order) => this.list.Add(order));

            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            this.list.Add(new Order { Id = "orderid" });

            var order = service.GetByIdWithDeleted("orderid");

            Assert.NotNull(order);
            Assert.Equal("orderid", order.Id);
        }

        [Fact]
        public void GetComplaintsShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Order>()))
               .Callback((Order order) => this.list.Add(order));

            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            var orderFake = new Order
            {
                Id = "orderid",
                Complaints = new List<Complaint>
                {
                    new Complaint { Id = "1" },
                    new Complaint { Id = "2" },
                    new Complaint { Id = "3" },
                },
            };

            this.list.Add(orderFake);

            var orderComplaints = service.GetComplaints("orderid");

            Assert.NotNull(orderComplaints);
            Assert.Equal(3, orderComplaints.Count());
        }

        [Fact]
        public void GetEmployeeOrdersShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Order>()))
               .Callback((Order order) => this.list.Add(order));

            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            var orderFake = new Order
            {
                Id = "orderid1",
                EmployeesOrder = new List<EmployeeOrder>
                {
                    new EmployeeOrder { EmployeeId = "1" },
                },
            };
            var orderFake2 = new Order
            {
                Id = "orderid2",
                EmployeesOrder = new List<EmployeeOrder>
                {
                    new EmployeeOrder { EmployeeId = "1" },
                },
            };

            this.list.Add(orderFake);
            this.list.Add(orderFake2);

            var employeeOrders = service.GetEmployeeOrdersByEmployeeId("1");

            Assert.NotNull(employeeOrders);
            Assert.Equal(2, employeeOrders.Count());
        }

        [Fact]
        public void GetUserOrdersShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Order>()))
               .Callback((Order order) => this.list.Add(order));

            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            var orderFake = new Order
            {
                ApplicationUserId = "user1",
            };
            var orderFake2 = new Order
            {
                ApplicationUserId = "user1",
            };

            this.list.Add(orderFake);
            this.list.Add(orderFake2);

            var employeeOrders = service.GetUserOrdersByUserId("user1");

            Assert.NotNull(employeeOrders);
            Assert.Equal(2, employeeOrders.Count());
        }

        [Fact]
        public void GetActiveOrderByUserIdShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Order>()))
               .Callback((Order order) => this.list.Add(order));

            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            var orderFake = new Order
            {
                Id = "orderactive",
                Status = FastServices.Data.Models.Enumerators.OrderStatus.Active,
                ApplicationUserId = "user1",
            };
            var orderFake2 = new Order
            {
                Id = "ordercompleted",
                Status = FastServices.Data.Models.Enumerators.OrderStatus.Completed,
                ApplicationUserId = "user1",
            };

            this.list.Add(orderFake);
            this.list.Add(orderFake2);

            var employeeOrders = service.GetActiveOrderByUserId("user1");

            Assert.NotNull(employeeOrders);
            Assert.Equal("orderactive", employeeOrders.Id);
        }

        [Fact]
        public void GetOrderFromInputModelShouldWorkCorrectly()
        {
            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            var model = new OrderInputModel
            {
                HoursBooked = 1,
                WorkersCount = 1,
                DateDate = DateTime.UtcNow.AddDays(1),
                DateHour = 12,
                ServiceId = 1,
                Address = "This is a correct addres example",
            };

            var order = new Order
            {
                BookedHours = model.HoursBooked,
                WorkersCount = model.WorkersCount,
                SubmitDate = DateTime.UtcNow,
                StartDate = model.StartDate.ToUniversalTime(),
                DueDate = model.DueDate.ToUniversalTime(),
                ServiceId = model.ServiceId,
                Status = FastServices.Data.Models.Enumerators.OrderStatus.Undefined,
                PaymentMethod = "Cash",
                Address = model.Address,
            };

            var newOrder = service.GetOrderFromInputModel(model);

            Assert.Equal(order.Address, newOrder.Address);
            Assert.Equal(order.PaymentMethod, newOrder.PaymentMethod);
            Assert.Equal(order.BookedHours, newOrder.BookedHours);
            Assert.Equal(order.Status, newOrder.Status);
            Assert.Equal(order.WorkersCount, newOrder.WorkersCount);
        }

        [Fact]
        public void HasAvailableEmployeesForTheOrderAsyncShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Order>()))
               .Callback((Order order) => this.list.Add(order));

            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            var availableEmployees = new List<Employee>
            {
                new Employee(),
                new Employee(),
                new Employee(),
            };
            var orderValid = new Order { WorkersCount = 3 };
            var orderInvalid = new Order { WorkersCount = 4 };

            var shouldHaveEnoughEmployees = service.HasAvailableEmployeesForTheOrderAsync(availableEmployees, orderValid);
            var shouldNotHaveEnoughEmployees = service.HasAvailableEmployeesForTheOrderAsync(availableEmployees, orderInvalid);

            Assert.True(shouldHaveEnoughEmployees);
            Assert.False(shouldNotHaveEnoughEmployees);
        }

        [Fact]
        public async Task ChangeOrderMethodShouldWorkCorrectly()
        {
            this.repository.Setup(r => r.All())
              .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Order>()))
               .Callback((Order order) => this.list.Add(order));

            var employeeService = new EmployeesService(this.repositoryEmployees.Object);
            var employeeOrdersService = new EmployeeOrdersService(employeeService, this.repositoryEmployeesOrders.Object);

            var service = new OrdersService(
                this.repository.Object,
                employeeOrdersService,
                employeeService);

            var orderFake = new Order
            {
                Id = "1",
                PaymentMethod = "cash",
            };
            var orderFake2 = new Order
            {
                Id = "2",
                PaymentMethod = "cash",
            };

            this.list.Add(orderFake);
            this.list.Add(orderFake2);

            await service.ChangeOrderPayment("1", "creditcard");
            var order = this.list.FirstOrDefault(x => x.Id == "1");

            Assert.Equal("creditcard", order.PaymentMethod);
        }

    }
}

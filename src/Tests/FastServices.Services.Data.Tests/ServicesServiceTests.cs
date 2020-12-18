using FastServices.Data.Common.Repositories;
using FastServices.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using FastServices.Services.Services;
using System.Threading.Tasks;
using FastServices.Web.ViewModels.Administration;

namespace FastServices.Services.Data.Tests
{
    public class ServicesServiceTests
    {
        private List<Service> list;
        private Mock<IDeletableEntityRepository<Service>> repository;

        public ServicesServiceTests()
        {
            this.list = new List<Service>();
            this.repository = new Mock<IDeletableEntityRepository<Service>>();
        }

        [Fact]
        public async Task AddAsyncShouldWorkProperly()
        {
            this.repository.Setup(r => r.All())
                           .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Service>()))
               .Callback((Service service) => this.list.Add(service));

            var servicesService = new ServicesService(this.repository.Object);

            var service = new Service();
            await servicesService.AddAsync(service);

            Assert.NotNull(this.list);
        }

        [Fact]
        public async Task AddServiceAsyncShouldWorkProperly()
        {
            this.repository.Setup(r => r.All())
                           .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Service>()))
               .Callback((Service service) => this.list.Add(service));

            var servicesService = new ServicesService(this.repository.Object);

            var serviceModel = new ServiceInputModel
            {
                Name = "name",
                DepartmentId = 2,
                Fee = 50.00M,
                Description = "description",
            };
            var uniqueFileName = "filename";

            await servicesService.AddServiceFromInputModelAsync(serviceModel, uniqueFileName);
            await servicesService.AddServiceFromInputModelAsync(serviceModel, string.Empty);

            Assert.NotNull(this.list.FirstOrDefault());
            Assert.Equal("name", this.list.FirstOrDefault().Name);
            Assert.Equal($"/images/{uniqueFileName}", this.list.FirstOrDefault().CardImgSrc);
            Assert.Equal($"/defaultImages/defBackgroundImg.png", this.list.LastOrDefault().CardImgSrc);
        }
    }
}

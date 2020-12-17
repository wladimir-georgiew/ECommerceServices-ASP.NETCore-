namespace FastServices.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Services.Complaints;
    using Moq;
    using Xunit;

    public class ComplaintsServiceTests
    {
        private List<Complaint> list;
        private Mock<IDeletableEntityRepository<Complaint>> repository;

        public ComplaintsServiceTests()
        {
            this.list = new List<Complaint>();
            this.repository = new Mock<IDeletableEntityRepository<Complaint>>();
        }

        [Fact]
        public async Task AddAsyncShouldAddCorrectly()
        {
            this.repository.Setup(r => r.All())
                .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            this.repository.Setup(r => r.AddAsync(It.IsAny<Complaint>()))
                .Callback((Complaint complaint) => this.list.Add(complaint));

            var service = new ComplaintsService(this.repository.Object);

            await service.AddComplaint(
                new Order
                {
                },
                new Web.ViewModels.Complaint.ComplaintInputModel
                {
                    Description = "test",
                });

            var complaint = this.list.FirstOrDefault(x => x.Description == "test");

            Assert.NotNull(complaint);
        }

        [Fact]
        public void GetAllShouldReturnAllWithoutDeleted()
        {
            this.repository.Setup(r => r.All())
                .Returns(this.list.AsQueryable().Where(x => x.IsDeleted == false));

            this.repository.Setup(r => r.AddAsync(It.IsAny<Complaint>()))
                .Callback((Complaint complaint) => this.list.Add(complaint));

            var service = new ComplaintsService(this.repository.Object);

            for (int i = 0; i < 4; i++)
            {
                this.list.Add(new Complaint { Id = i.ToString(), IsDeleted = i % 2 == 0 ? true : false, });
            }

            var all = service.GetAll();

            Assert.Equal(2, all.Count());
        }

        [Fact]
        public void GetAllWithDeletedShouldReturnAll()
        {
            this.repository.Setup(r => r.AllWithDeleted())
                .Returns(this.list.AsQueryable());

            this.repository.Setup(r => r.AddAsync(It.IsAny<Complaint>()))
                .Callback((Complaint complaint) => this.list.Add(complaint));

            var service = new ComplaintsService(this.repository.Object);

            for (int i = 0; i < 4; i++)
            {
                this.list.Add(new Complaint { Id = i.ToString(), IsDeleted = i % 2 == 0 ? true : false, });
            }

            var all = service.GetAllWithDeleted();

            Assert.Equal(4, all.Count());
        }
    }
}

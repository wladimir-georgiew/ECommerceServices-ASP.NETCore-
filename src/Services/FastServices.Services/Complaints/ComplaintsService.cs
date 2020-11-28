namespace FastServices.Services.Complaints
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FastServices.Data.Common.Repositories;
    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Complaint;

    public class ComplaintsService : IComplaintsService
    {
        private readonly IDeletableEntityRepository<Complaint> repository;

        public ComplaintsService(IDeletableEntityRepository<Complaint> repository)
        {
            this.repository = repository;
        }

        public IQueryable<Complaint> GetAll() => this.repository.All();

        public IQueryable<Complaint> GetAllWithDeleted() => this.repository.AllWithDeleted();

        public async Task AddComplaint(Order order, ComplaintInputModel input)
        {
            Complaint complaint = new Complaint
            {
                Description = input.Description,
                OrderId = input.OrderId,
            };

            order.Complaints.Add(complaint);

            await this.repository.AddAsync(complaint);

            await this.repository.SaveChangesAsync();
        }
    }
}

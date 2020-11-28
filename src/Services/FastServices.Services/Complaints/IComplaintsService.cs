namespace FastServices.Services.Complaints
{
    using System.Linq;
    using System.Threading.Tasks;
    using FastServices.Data.Models;
    using FastServices.Web.ViewModels.Complaint;

    public interface IComplaintsService
    {
        IQueryable<Complaint> GetAll();

        IQueryable<Complaint> GetAllWithDeleted();

        public Task AddComplaint(Order order, ComplaintInputModel input);
    }
}

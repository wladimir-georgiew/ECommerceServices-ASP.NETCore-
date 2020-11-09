namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Models;
    using FastServices.Services.Departments;
    using FastServices.Web.ViewModels.Comments;
    using FastServices.Web.ViewModels.Departments;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ApplicationDbContext db;

        public DepartmentsController(IDepartmentsService departmentsService, ApplicationDbContext db)
        {
            this.departmentsService = departmentsService;
            this.db = db;
        }

        public IActionResult Department(int id)
        {
            string bgUrl = this.db.Departments.FirstOrDefault(x => x.Id == id).BackgroundImgSrc;
            string depName = this.db.Departments.FirstOrDefault(x => x.Id == id).Name;

            List<ServicesViewModel> servicesViewModel = this.departmentsService.GetDepartmentServices(id)
                .Select(x => new ServicesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CardImgSrc = x.CardImgSrc,
                    DepartmentId = x.DepartmentId,
                })
                .ToList();

            this.ViewData["url"] = bgUrl;
            this.ViewData["depName"] = depName.ToUpper();

            var model = new Tuple<
                ICollection<ServicesViewModel>, OrderModalViewModel
                >(servicesViewModel, new OrderModalViewModel());

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Department(OrderModalViewModel model)
        {
            // add a custom error
            this.ModelState.AddModelError(string.Empty, "This is a custom error for testing!");

            // check the model (you should also do front-end vlidation, as in the demo)
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // do your stuff


            // add a success user message
            model.ResultMessage = "Your form has been submitted.";

            return this.View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(CommentInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var comment = new Comment
            {
                CommentContent = input.CommentContent,
                Stars = input.Stars,
                CreatedOn = DateTime.UtcNow,
                DepartmentId = input.DepartmentId,
                IsDeleted = false,
                ApplicationUserId = userId,
            };

            await this.db.Comments.AddAsync(comment);
            await this.db.SaveChangesAsync();

            int id = input.DepartmentId;

            return this.RedirectToAction("Department", new { id });
        }
    }
}

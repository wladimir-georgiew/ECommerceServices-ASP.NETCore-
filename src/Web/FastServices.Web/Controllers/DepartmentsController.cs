namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FastServices.Data;
    using FastServices.Data.Models;
    using FastServices.Services.Comments;
    using FastServices.Services.Departments;
    using FastServices.Services.Users;
    using FastServices.Web.ViewModels.Comments;
    using FastServices.Web.ViewModels.Departments;
    using FastServices.Web.ViewModels.PaginationList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentsController : Controller
    {
        private const int CommentsPerPage = 3;

        private readonly IDepartmentsService departmentsService;
        private readonly IUsersService usersService;

        public DepartmentsController(IDepartmentsService departmentsService, IUsersService usersService)
        {
            this.departmentsService = departmentsService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Department(int depId, int pageNumber = 1)
        {
            // Services model
            Department department = await this.departmentsService.GetDepartmentByIdAsync(depId);

            if (department == null)
            {
                return this.NotFound();
            }

            string bgUrl = department.BackgroundImgSrc;
            string depName = department.Name;

            this.ViewData["topImageNavUrl"] = bgUrl;
            this.ViewData["title"] = depName.ToUpper();

            List<ServiceViewModel> servicesViewModel = this.departmentsService.GetDepartmentServices(depId)
                .Select(x => new ServiceViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CardImgSrc = x.CardImgSrc,
                    DepartmentId = x.DepartmentId,
                })
                .ToList();

            // Comments model
            var departmentComments = this.departmentsService.GetAllDepartments()
                .Where(x => x.Id == depId)
                .SelectMany(x => x.Comments)
                .ToList()
                .OrderByDescending(x => x.CreatedOn);

            var commentsViewModel = departmentComments.Select(x => new CommentViewModel()
            {
                CommentContent = x.CommentContent,
                CreatedOn = x.CreatedOn.ToString(format: "d"),
                Name = this.usersService.GetByIdWithDeletedAsync(x.ApplicationUserId).GetAwaiter().GetResult().Name,
                AvatarImgSrc = this.usersService.GetByIdWithDeletedAsync(x.ApplicationUserId).GetAwaiter().GetResult().AvatarImgSrc,
                Stars = x.Stars,
                DepartmentId = depId,
                UserId = x.ApplicationUserId,
                CommentId = x.Id,
            })
                .ToList();

            // Model
            var model = new DepartmentViewModel
            {
                // Department services
                ServicesViewModel = servicesViewModel,

                // Department comments
                CommentsMasterModel = new CommentsMasterModel
                {
                    ViewModel = PaginationList<CommentViewModel>.Create(commentsViewModel, pageNumber, CommentsPerPage),
                    InputModel = new CommentInputModel(),
                },
            };

            return this.View(model);
        }
    }
}

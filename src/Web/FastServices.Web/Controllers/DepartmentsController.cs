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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;

        public DepartmentsController(IDepartmentsService departmentsService, ICommentsService commentsService, IUsersService usersService)
        {
            this.departmentsService = departmentsService;
            this.commentsService = commentsService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Department(int id)
        {
            // Services model
            Department department = await this.departmentsService.GetDepartmentByIdAsync(id);

            string bgUrl = department.BackgroundImgSrc;
            string depName = department.Name;

            this.TempData["messageValue"] = FastServices.Common.GlobalConstants.SuccessCommentPostMessage;
            this.ViewData["topImageNavUrl"] = bgUrl;
            this.ViewData["depName"] = depName.ToUpper();

            List<ServiceViewModel> servicesViewModel = this.departmentsService.GetDepartmentServices(id)
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
                .Where(x => x.Id == id)
                .SelectMany(x => x.Comments)
                .ToList()
                .OrderByDescending(x => x.CreatedOn)
                .Take(5);

            var commentsViewModel = departmentComments.Select(x => new CommentViewModel()
            {
                CommentContent = x.CommentContent,
                CreatedOn = x.CreatedOn.ToString(format: "d"),
                Name = this.usersService.GetByIdWithDeletedAsync(x.ApplicationUserId).GetAwaiter().GetResult().Name,
                AvatarImgSrc = this.usersService.GetByIdWithDeletedAsync(x.ApplicationUserId).GetAwaiter().GetResult().AvatarImgSrc,
                Stars = x.Stars,
                DepartmentId = id,
                UserId = x.ApplicationUserId,
                CommentId = x.Id,
            })
                .ToList();

            // Model
            var model = new DepartmentViewModel();

            model.ServicesViewModel = servicesViewModel;

            model.CommentsMasterModel = new CommentsMasterModel
            {
                ViewModel = commentsViewModel,
                InputModel = new CommentInputModel(),
            };

            return this.View(model);
        }
    }
}

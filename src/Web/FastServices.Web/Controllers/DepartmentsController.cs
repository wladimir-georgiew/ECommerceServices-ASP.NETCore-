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
            Department department = await this.departmentsService.GetDepartmentByIdAsync(id);

            string bgUrl = department.BackgroundImgSrc;
            string depName = department.Name;

            this.TempData["messageValue"] = FastServices.Common.GlobalConstants.SuccessCommentPostMessage;
            this.ViewData["topImageNavUrl"] = bgUrl;
            this.ViewData["depName"] = depName.ToUpper();

            List<ServicesViewModel> servicesViewModel = this.departmentsService.GetDepartmentServices(id)
                .Select(x => new ServicesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CardImgSrc = x.CardImgSrc,
                    DepartmentId = x.DepartmentId,
                })
                .ToList();

            return this.View(servicesViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NoContent();
            }
            else
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Don't proceed if 24 hours haven't passed since last comment (spam protection)
                if (!this.usersService.IsUserAllowedToComment(userId))
                {
                    return this.Redirect($"Department?id={input.DepartmentId}&submit=false");
                }

                var comment = new Comment
                {
                    CommentContent = input.CommentContent,
                    Stars = input.Stars,
                    CreatedOn = DateTime.UtcNow,
                    DepartmentId = input.DepartmentId,
                    ApplicationUserId = userId,
                };

                await this.commentsService.AddCommentAsync(comment);
            }

            return this.Redirect($"Department?id={input.DepartmentId}&submit=true");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteComment(int departmentId, int commentId)
        {
            var comment = this.commentsService.GetCommentById(commentId);

            this.commentsService.HardDeleteComment(comment);

            this.TempData["Message"] = "You have deleted your comment successfully!";

            return this.NoContent();
        }
    }
}

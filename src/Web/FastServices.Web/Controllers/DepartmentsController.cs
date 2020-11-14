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
        private readonly IDepartmenstService departmentsService;
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;

        public DepartmentsController(IDepartmenstService departmentsService, ICommentsService commentsService, IUsersService usersService)
        {
            this.departmentsService = departmentsService;
            this.commentsService = commentsService;
            this.usersService = usersService;
        }

        public IActionResult Department(int id)
        {
            Department department = this.departmentsService.GetDepartment(id);

            string bgUrl = department.BackgroundImgSrc;
            string depName = department.Name;

            this.ViewData["url"] = bgUrl;
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
                this.TempData["Message"] = this.ModelState
                    .Values
                    .SelectMany(modelState => modelState.Errors)
                    .FirstOrDefault()
                    .ErrorMessage;

                return this.Redirect($"Department?id={input.DepartmentId}&submit=false");
            }
            else
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

                // Don't proceed if 24 hours haven't passed since last comment (spam protection)
                if (!this.usersService.IsUserAllowedToComment(userId))
                {
                    this.TempData["Message"] = "You need to wait 24 hours before posting new  comment!";

                    return this.Redirect($"Department?id={input.DepartmentId}&submit=false");
                }

                await this.commentsService.AddComment(comment);

                this.TempData["Message"] = "Thank you for your feedback!";
            }

            return this.Redirect($"Department?id={input.DepartmentId}&submit=true");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int departmentId, int commentId)
        {
            var comment = this.commentsService.GetCommentById(commentId);

            await this.commentsService.DeleteComment(comment);

            this.TempData["Message"] = "You have deleted your comment successfully!";

            return this.Redirect($"Department?id={departmentId}&submit=true");
        }
    }
}

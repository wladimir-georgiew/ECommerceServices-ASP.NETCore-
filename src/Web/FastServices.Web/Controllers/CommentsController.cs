namespace FastServices.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FastServices.Common;
    using FastServices.Data.Models;
    using FastServices.Services.Comments;
    using FastServices.Services.Departments;
    using FastServices.Services.Users;
    using FastServices.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly IDepartmentsService departmentsService;
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;

        public CommentsController(IDepartmentsService departmentsService, ICommentsService commentsService, IUsersService usersService)
        {
            this.departmentsService = departmentsService;
            this.commentsService = commentsService;
            this.usersService = usersService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Departments/Department?depId={input.DepartmentId}");
            }
            else
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Don't proceed if 24 hours haven't passed since last comment (spam protection)
                if (!this.usersService.IsUserAllowedToComment(userId))
                {
                    this.TempData["msg"] = GlobalConstants.ErrorCommentPostSpamMessage;
                    return this.Redirect($"/Departments/Department?depId={input.DepartmentId}");
                }

                var comment = new Comment
                {
                    CommentContent = input.CommentContent.Trim(),
                    Stars = input.Stars,
                    CreatedOn = DateTime.UtcNow,
                    DepartmentId = input.DepartmentId,
                    ApplicationUserId = userId,
                };

                await this.commentsService.AddCommentAsync(comment);
            }

            this.TempData["msg"] = GlobalConstants.SuccessCommentPostMessage;
            return this.Redirect($"/Departments/Department?depId={input.DepartmentId}");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int departmentId, int commentId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var comment = this.commentsService.GetById(commentId);

            if (!this.User.IsInRole("Administrator") &&
                comment.ApplicationUserId != userId)
            {
                return this.Forbid();
            }

            await this.commentsService.DeleteCommentAsync(comment);

            return this.NoContent();
        }
    }
}

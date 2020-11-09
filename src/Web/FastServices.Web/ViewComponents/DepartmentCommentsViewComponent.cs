﻿namespace FastServices.Web.ViewComponents
{
    using System;
    using System.Linq;

    using FastServices.Data;
    using FastServices.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Mvc;

    public class DepartmentCommentsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public DepartmentCommentsViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        // id = departmentid
        public IViewComponentResult Invoke(int id)
        {
            var departmentComments = this.db.Departments
                .Where(x => x.Id == id)
                .SelectMany(x => x.Comments)
                .ToList()
                .OrderByDescending(x => x.CreatedOn);

            var viewModel = departmentComments.Select(x => new CommentViewModel()
            {
                CommentContent = x.CommentContent,
                CreatedOn = x.CreatedOn.ToString(format: "d"),
                // maybe fix
                Name = this.db.Users.Where(u => u.Id == x.ApplicationUserId).Select(u => u.Name).FirstOrDefault(),
                AvatarImgSrc = this.db.Users.Where(u => u.Id == x.ApplicationUserId).Select(u => u.AvatarImgSrc).FirstOrDefault(),
                Stars = x.Stars,
                DepartmentId = id,
            }).ToList();

            return this.View(viewModel);
        }
    }
}

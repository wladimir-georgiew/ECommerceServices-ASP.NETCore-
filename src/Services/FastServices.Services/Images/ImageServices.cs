namespace FastServices.Services
{
    using System;
    using System.IO;

    using FastServices.Services.Images;
    using FastServices.Web.ViewModels.Employees;
    using Microsoft.AspNetCore.Hosting;

    public class ImageServices : IImageServices
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public ImageServices(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string GetUploadedFileName(EmployeeInputModel model)
        {
            if (model.ProfileImage == null)
            {
                return string.Empty;
            }

            string uploadsFolder = Path.Combine(this.hostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            model.ProfileImage.CopyTo(fileStream);

            return uniqueFileName;
        }
    }
}

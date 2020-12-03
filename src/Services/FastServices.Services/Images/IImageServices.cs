namespace FastServices.Services.Images
{
    using FastServices.Web.ViewModels.Employees;
    using Microsoft.AspNetCore.Http;

    public interface IImageServices
    {
        public string GetUploadedFileName(IFormFile file);
    }
}

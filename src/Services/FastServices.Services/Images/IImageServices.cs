namespace FastServices.Services.Images
{
    using FastServices.Web.ViewModels.Employees;

    public interface IImageServices
    {
        public string GetUploadedFileName(EmployeeInputModel model);
    }
}

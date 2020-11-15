//namespace FastServices.Web.Areas.Administration.Controllers
//{
//    using FastServices.Services.Data;
//    using FastServices.Web.ViewModels.Administration.Dashboard;

//    using Microsoft.AspNetCore.Mvc;

//    public class DashboardController : AdministrationController
//    {
//        private readonly ISettingsService settingsService;

//        public DashboardController(ISettingsService settingsService)
//        {
//            this.settingsService = settingsService;
//        }

//        public IActionResult Index()
//        {
//            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
//            return this.View(viewModel);
//        }
//    }
//}

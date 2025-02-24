using Microsoft.AspNetCore.Mvc;

namespace CaseDiaryView.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

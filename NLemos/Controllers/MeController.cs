using Microsoft.AspNetCore.Mvc;

namespace NLemos.Controllers
{
    public class MeController : Controller
    {
        [ResponseCache(Duration = 3600 * 24, Location = ResponseCacheLocation.Client)] //one day
        public IActionResult CV()
        {
            return View();
        }
    }
}
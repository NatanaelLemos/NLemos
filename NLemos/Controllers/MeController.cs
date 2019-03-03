using Microsoft.AspNetCore.Mvc;
using NLemos.Domain.Services;
using System.Threading.Tasks;

namespace NLemos.Controllers
{
    public class MeController : Controller
    {
        private readonly ICreatorService _service;

        public MeController(ICreatorService service)
        {
            _service = service;
        }

        //[ResponseCache(Duration = 3600 * 24, Location = ResponseCacheLocation.Client)] //one day
        public async Task<IActionResult> CV()
        {
            ViewBag.Resume = await _service.GetResume();
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using NLemos.Domain.Entities;
using NLemos.Domain.Services;

namespace NLemos.Controllers
{
    public class ControlController : Controller
    {
        private readonly IPostService _postService;

        public ControlController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            _postService.CreatePost(post);
            return RedirectToAction("AddPost");
        }
    }
}
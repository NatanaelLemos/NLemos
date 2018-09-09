using Microsoft.AspNetCore.Mvc;
using NLemos.Domain.Entities;
using NLemos.Domain.Services;
using System.Threading.Tasks;

namespace NLemos.Controllers
{
    public class ControlController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICreatorService _creatorService;

        public ControlController(IPostService postService, ICreatorService creatorService)
        {
            _postService = postService;
            _creatorService = creatorService;
        }

        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(string key, Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            var validKey = await _creatorService.ValidateKey(key);
            if (validKey)
            {
                //only add new post if key is valid
                _postService.CreatePost(post);
            }

            return RedirectToAction("AddPost");
        }
    }
}
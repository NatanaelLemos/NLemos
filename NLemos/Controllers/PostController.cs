using Microsoft.AspNetCore.Mvc;
using NLemos.Domain.Services;
using System.Threading.Tasks;

namespace NLemos.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [ResponseCache(Duration = 3600 * 6, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new string[] { "number" })] //six hours
        public async Task<IActionResult> Page(int number)
        {
            ViewBag.Title = "Natanael Lemos";
            ViewBag.Summary = "Um pouco sobre o mundo .Net e tecnologia em geral";
            ViewBag.PageNumber = number;
            var posts = await _postService.ReadPage(number);
            return View(posts);
        }

        [ResponseCache(Duration = 3600 * 24, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new string[] { "hashName" })] //one day
        public async Task<IActionResult> Read(string hashName)
        {
            var fullPost = await _postService.ReadFullPost(hashName);

            ViewBag.Title = fullPost.Title;
            ViewBag.Summary = fullPost.Summary;
            ViewBag.Url = $"//nlemos.azurewebsites.net/Read/{hashName}";
            return View(fullPost);
        }

        [ResponseCache(Duration = 3600 * 6, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new string[] { "args" })] //six hours
        public async Task<IActionResult> Search(string args)
        {
            return View();
            //ViewBag.Title = "Resultado da pesquisa";

            //var searchResult = (await _searchService.Search(args)).OrderBy(s => s.Score);
            //return View(searchResult.Select(r => new domain.entities.Post
            //{
            //    Id = r.Id,
            //    HashName = r.HashName,
            //    Title = r.Title,
            //    Summary = r.Summary,
            //    FullPost = "",
            //    PostDate = r.PostDate
            //}));
        }
    }
}
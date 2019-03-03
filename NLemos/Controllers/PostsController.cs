using Microsoft.AspNetCore.Mvc;
using NLemos.Domain.Entities;
using NLemos.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NLemos.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ISearchService _searchService;

        public PostsController(IPostService postService, ISearchService searchService)
        {
            _postService = postService;
            _searchService = searchService;
        }

        [ResponseCache(Duration = 3600 * 6, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new string[] { "id" })] //six hours
        public async Task<IActionResult> Page(int id = 0)
        {
            ViewBag.Title = "Natanael Lemos";
            ViewBag.Summary = "";
            ViewBag.PageNumber = id;
            var posts = await _postService.ReadPage(id);
            return View(posts);
        }

        [ResponseCache(Duration = 3600 * 24, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new string[] { "id" })] //one day
        public async Task<IActionResult> Read(string id)
        {
            var fullPost = await _postService.ReadFullPost(id);

            if (fullPost == null)
            {
                fullPost = new Post
                {
                    Title = "Artigo não encontrado",
                    Summary = "Esse artigo não está disponível no momento",
                    HashName = "notfound",
                    PostDate = DateTime.Now
                };
            }

            ViewBag.Title = fullPost.Title;
            ViewBag.Summary = fullPost.Summary;
            ViewBag.Url = $"//nlemos.azurewebsites.net/Read/{id}";

            return View(fullPost);
        }

        [ResponseCache(Duration = 3600 * 6, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new string[] { "id" })] //six hours
        public async Task<IActionResult> Search(string id)
        {
            ViewBag.Title = "Resultado da pesquisa";

            var searchResult = await _searchService.Search(id);
            return View(searchResult.Select(r => new Post
            {
                HashName = r.HashName,
                Title = r.Title,
                Summary = r.Summary,
                PostDate = r.PostDate
            }));
        }
    }
}
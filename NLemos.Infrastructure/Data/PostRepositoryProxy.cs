using NLemos.Domain.Data;
using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLemos.Infrastructure.Data
{
    public class PostRepositoryProxy : IPostRepository
    {
        private readonly IPostRepository _instance;
        private readonly PostRepositoryCache _cache;

        public PostRepositoryProxy(BlogContext ctx, PostRepositoryCache cache)
        {
            _instance = new PostRepository(ctx);
            _cache = cache;
        }

        public void Create(Post post)
        {
            _instance.Create(post);
        }

        public async Task<Post> ReadFullPost(string hashName)
        {
            var cache = _cache[hashName];

            if (cache == null)
            {
                var post = await _instance.ReadFullPost(hashName);
                _cache[hashName] = post;
                return post;
            }
            else
            {
                return cache;
            }
        }

        public Task<List<Post>> SkipTake(int skip, int take)
        {
            return _instance.SkipTake(skip, take);
        }

        public Task<List<Post>> Search(string text)
        {
            return _instance.Search(text);
        }

        public void Dispose()
        {
            _instance.Dispose();
        }
    }
}

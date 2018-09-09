using NLemos.Domain.Data;
using NLemos.Domain.Entities;
using NLemos.Domain.Extensions;
using NLemos.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NLemos.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Post>> ReadPage(int pageNumber)
        {
            const int take = 15;
            var skip = take * pageNumber;
            return _repository.SkipTake(skip, take);
        }

        public Task<Post> ReadFullPost(string hashName)
        {
            return _repository.ReadFullPost(hashName);
        }

        public void CreatePost(Post post)
        {
            post.HashName = post.Title.GenerateHash();

            foreach (var tag in post.Tags)
            {
                tag.HashName = tag.Name.GenerateHash();
            }

            _repository.Create(post);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
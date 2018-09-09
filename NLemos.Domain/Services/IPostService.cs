using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NLemos.Domain.Services
{
    public interface IPostService : IDisposable
    {
        Task<List<Post>> ReadPage(int pageNumber);

        Task<Post> ReadFullPost(string hashName);
        void CreatePost(Post post);
    }
}
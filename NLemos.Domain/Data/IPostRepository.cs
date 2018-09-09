using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NLemos.Domain.Data
{
    public interface IPostRepository : IDisposable
    {
        void Create(Post post);

        Task<List<Post>> SkipTake(int skip, int take);

        Task<Post> ReadFullPost(string hashName);
    }
}
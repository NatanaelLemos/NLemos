using NLemos.Domain.Data;
using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLemos.Infrastructure.Data
{
    public class CreatorRepositoryProxy : ICreatorRepository
    {
        private readonly ICreatorRepository _instance;
        private readonly CreatorCache _cache;

        public CreatorRepositoryProxy(BlogContext ctx, CreatorCache cache)
        {
            _instance = new CreatorRepository(ctx);
            _cache = cache;
        }

        public async Task<Creator> Get()
        {
            var cache = _cache["creator"];

            if (cache == null)
            {
                var creator = await _instance.Get();
                _cache["creator"] = creator;
                return creator;
            }
            else
            {
                return cache;
            }
        }
    }
}

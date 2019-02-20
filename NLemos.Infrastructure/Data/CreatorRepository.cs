using MongoDB.Driver;
using NLemos.Domain.Data;
using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLemos.Infrastructure.Data
{
    public class CreatorRepository : ICreatorRepository
    {
        private readonly BlogContext _ctx;

        public CreatorRepository(BlogContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Creator> Get()
        {
            return _ctx.Creators.Find(Builders<Creator>.Filter.Empty).FirstOrDefaultAsync();
        }

        public Task<List<ResumeSection>> GetResume()
        {
            return _ctx.Resume.Find(Builders<ResumeSection>.Filter.Empty).ToListAsync();
        }
    }
}

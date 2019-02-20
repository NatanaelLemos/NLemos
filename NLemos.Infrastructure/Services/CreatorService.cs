using NLemos.Domain.Data;
using NLemos.Domain.Entities;
using NLemos.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLemos.Infrastructure.Services
{
    public class CreatorService : ICreatorService
    {
        private readonly ICreatorRepository _repository;

        public CreatorService(ICreatorRepository repository)
        {
            _repository = repository;
        }

        public Task<Creator> Get()
        {
            return _repository.Get();
        }

        public async Task<bool> ValidateKey(string key)
        {
            var creator = await Get();
            return creator.Key == key;
        }

        public Task<List<ResumeSection>> GetResume()
        {
            return _repository.GetResume();
        }
    }
}

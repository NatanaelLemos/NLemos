using NLemos.Domain.Data;
using NLemos.Domain.Entities;
using NLemos.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLemos.Infrastructure.Services
{
    public class SearchService : ISearchService
    {
        private readonly IPostRepository _repository;

        public SearchService(IPostRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Post>> Search(string args)
        {
            return _repository.Search(args);
        }

        public void Dispose()
        {
        }
    }
}

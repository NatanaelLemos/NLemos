using NLemos.Domain.Entities;

namespace NLemos.Infrastructure.Data
{
    public class CreatorCache : DataCache<Creator>
    {
        public CreatorCache() : base(168)
        {

        }
    }
}

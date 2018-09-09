using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLemos.Domain.Data
{
    public interface ICreatorRepository
    {
        Task<Creator> Get();
    }
}

using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLemos.Domain.Services
{
    public interface ICreatorService
    {
        Task<Creator> Get();
        Task<bool> ValidateKey(string key);
    }
}

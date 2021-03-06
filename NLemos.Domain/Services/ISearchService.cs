﻿using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NLemos.Domain.Services
{
    public interface ISearchService : IDisposable
    {
        Task<List<Post>> Search(string args);
    }
}
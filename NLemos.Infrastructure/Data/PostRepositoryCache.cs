using NLemos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLemos.Infrastructure.Data
{
    public class PostRepositoryCache
    {
        private readonly Dictionary<string, PostCache> _cache = new Dictionary<string, PostCache>();

        public Post this[string key]
        {
            get
            {
                if (!_cache.ContainsKey(key)) return null;
                var post = _cache[key];
                if (post.Creation < DateTime.Now.AddHours(-24)) return null;
                return post.Post;
            }
            set
            {
                if (_cache.ContainsKey(key))
                {
                    _cache[key] = new PostCache(value, DateTime.Now);
                }
                else
                {
                    _cache.Add(key, new PostCache(value, DateTime.Now));
                }
            }
        }

        private class PostCache
        {
            public Post Post { get; }
            public DateTime Creation { get; }

            public PostCache(Post post, DateTime creation)
            {
                Post = post;
                Creation = creation;
            }
        }
    }
}

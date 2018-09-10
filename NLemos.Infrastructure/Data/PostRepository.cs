using MongoDB.Driver;
using NLemos.Domain.Data;
using NLemos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NLemos.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private const string POST_INDEX = "postindex";
        private readonly BlogContext _ctx;

        public PostRepository(BlogContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Post post)
        {
            if (_ctx.Posts.Find(Builders<Post>.Filter.Eq(p => p.HashName, post.HashName)).Any())
            {
                throw new MongoDuplicateKeyException(null, "Title already in use", null);
            }

            _ctx.Posts.InsertOne(post);

            foreach (var tag in post.Tags)
            {
                var currTag = _ctx.Tags.Find(Builders<Domain.Entities.Tag>.Filter.Eq(t => t.HashName, tag.HashName)).FirstOrDefault();
                var emptyPost = new Post { HashName = post.HashName, Title = post.Title };
                if (currTag == null)
                {
                    tag.Posts.Add(emptyPost);
                    _ctx.Tags.InsertOne(tag);
                }
                else
                {
                    currTag.Posts.Add(emptyPost);
                    _ctx.Tags.FindOneAndReplace(Builders<Domain.Entities.Tag>.Filter.Eq(t => t.Name, tag.Name), currTag);
                }
            }

            _ctx.Posts.Indexes.CreateOne(
                new CreateIndexModel<Post>(
                    Builders<Post>.IndexKeys.Text(_ => _.FullPost)
                )
            );
        }

        public Task<Post> ReadFullPost(string hashName)
        {
            return _ctx.Posts
                        .Find(Builders<Post>.Filter.Eq(p => p.HashName, hashName))
                        .FirstOrDefaultAsync();
        }

        public Task<List<Post>> SkipTake(int skip, int take)
        {
            return _ctx.Posts
                .Find(Builders<Post>.Filter.Empty)
                .SortByDescending(p => p.PostDate)
                .Skip(skip)
                .Limit(take)
                .Project<Post>(
                    Builders<Post>.Projection
                        .Include(p => p.HashName)
                        .Include(p => p.Title)
                        .Include(p => p.Summary)
                        .Include(p => p.PostDate)
                )
                .ToListAsync();
        }

        public Task<List<Post>> Search(string text)
        {
            return _ctx.Posts
                    .Find(Builders<Post>.Filter.Text(text))
                    .Limit(15)
                    .Project<Post>(
                        Builders<Post>.Projection
                            .Include(p => p.HashName)
                            .Include(p => p.Title)
                            .Include(p => p.Summary)
                            .Include(p => p.PostDate)
                    )
                    .ToListAsync();
        }

        public void Dispose()
        {
        }
    }
}
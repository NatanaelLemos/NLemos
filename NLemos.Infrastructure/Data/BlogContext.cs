using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NLemos.Domain.Entities;

namespace NLemos.Infrastructure.Data
{
    public class BlogContext
    {
        private IMongoDatabase _database { get; }

        public BlogContext(string connectionString)
        {
            MapModels();

            var url = new MongoUrl(connectionString);
            MongoClientSettings settings = MongoClientSettings.FromUrl(url);
            settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            _database = mongoClient.GetDatabase(url.DatabaseName);

            SeedCreator();
        }

        private void MapModels()
        {
            BsonClassMap.RegisterClassMap<Post>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Domain.Entities.Tag>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Domain.Entities.Creator>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }

        private void SeedCreator()
        {
            if (Creators.Find(Builders<Creator>.Filter.Empty).Any()) return;

            Creators.InsertOne(new Creator());
        }

        private IMongoCollection<T> GetCollection<T>(string collectionName) => _database.GetCollection<T>(collectionName);

        public IMongoCollection<Post> Posts => GetCollection<Post>("posts");
        public IMongoCollection<Domain.Entities.Tag> Tags => GetCollection<Domain.Entities.Tag>("tags");

        public IMongoCollection<Creator> Creators => GetCollection<Creator>("creators");
    }
}
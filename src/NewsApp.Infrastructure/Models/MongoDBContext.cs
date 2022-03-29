using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace NewsApp.Infrastructure.Models
{

    public class MongoDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            var connectionString = _configuration.GetValue<string>("MongoDBConfiguration:ConnectionString");
            var db = _configuration.GetValue<string>("MongoDBConfiguration:Database");
            var client = new MongoClient(connectionString);
            _mongoDatabase = client.GetDatabase(db);
        }

        public IMongoCollection<Channel> Channel => _mongoDatabase.GetCollection<Channel>(nameof(Channel));
        public IMongoCollection<Category> Category => _mongoDatabase.GetCollection<Category>(nameof(Category));
        public IMongoCollection<User> User => _mongoDatabase.GetCollection<User>(nameof(User));
        public IMongoCollection<News> News => _mongoDatabase.GetCollection<News>(nameof(News));
        public IMongoCollection<NewsView> NewsView => _mongoDatabase.GetCollection<NewsView>(nameof(NewsView));
        public IMongoCollection<NewsCommentNPoint> NewsCommentNPoint => _mongoDatabase.GetCollection<NewsCommentNPoint>(nameof(NewsCommentNPoint));
        public IMongoCollection<NewsTag> NewsTag => _mongoDatabase.GetCollection<NewsTag>(nameof(NewsTag));
        public IMongoCollection<NewsImage> NewsImage => _mongoDatabase.GetCollection<NewsImage>(nameof(NewsImage));
        public IMongoCollection<ChannelCategoryMap> ChannelCategoryMap => _mongoDatabase.GetCollection<ChannelCategoryMap>(nameof(ChannelCategoryMap));
        public IMongoCollection<NewsNewsTagMap> NewsNewsTagMap => _mongoDatabase.GetCollection<NewsNewsTagMap>(nameof(NewsNewsTagMap));
        public IMongoCollection<UserInterest> UserInterest => _mongoDatabase.GetCollection<UserInterest>(nameof(UserInterest));
        public IMongoCollection<SearchHistory> SearchHistory => _mongoDatabase.GetCollection<SearchHistory>(nameof(SearchHistory));
    }
}
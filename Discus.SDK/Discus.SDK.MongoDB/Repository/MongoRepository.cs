using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discus.SDK.MongoDB.Repository
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoClient _mongoClient;

        private readonly IMongoDatabase _database;

        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _database = mongoClient.GetDatabase("discus");
            _collection = _database.GetCollection<BsonDocument>("logger");
        }

        public async Task<BsonDocument> GetAsync()
        {
            FilterDefinition<BsonDocument> filter = null;
            FindOptions<BsonDocument>? options = null;
            return await GetAsync(filter, options);
        }

        private async Task<BsonDocument> GetAsync(FilterDefinition<BsonDocument> filter, FindOptions<BsonDocument>? options = null, CancellationToken cancellationToken = default)
        {
            var cursor = await _collection.FindAsync(filter, options, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}

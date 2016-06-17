using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess
{
    class MongoDbContext
    {
        public MongoDbContext()
        {
            var connectionString = ConfigurationManager.AppSettings["mongodb:ConnectionString"];
            var database = ConfigurationManager.AppSettings["mongodb:Database"];
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(database);
            
            Properties = mongoDatabase.GetCollection<BsonDocument>("Properties");
        }

        public IMongoCollection<BsonDocument> Properties { get; set; }

        public IMongoCollection<BsonDocument> Communications { get; set; }
    }
}
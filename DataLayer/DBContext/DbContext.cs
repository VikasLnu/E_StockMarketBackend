using Microsoft.Extensions.Options;
using Model.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBContext
{
   public class DbContext : IDbContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public DbContext(IOptions<MongoDBSettings> configuration)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(configuration.Value.ConnectionURI));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
           
            _mongoClient = new MongoClient(settings);
            
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}

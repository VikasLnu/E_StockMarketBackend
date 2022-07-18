using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    [BsonIgnoreExtraElements]
    public  class Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CompanyCode { get; set; }
        public string ComapanyName { get; set; }
        public string CompanyCEO { get; set; }
        public string CompanyTurnover { get; set; }
        public string CompanyWebsite { get; set; }
        public string Exchange { get; set; }
    }
}

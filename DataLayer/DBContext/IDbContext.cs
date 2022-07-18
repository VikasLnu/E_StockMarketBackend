using DataLayer.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBContext
{
  public  interface IDbContext 
    {
        public IMongoCollection<T> GetCollection<T>(string name);
       // IMongoCollection<Company> GetCollection<Company>(string name);
    }
}

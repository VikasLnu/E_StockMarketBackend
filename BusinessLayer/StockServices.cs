using AutoMapper;
using BusinessLayer.Interfaces;
using DataLayer.DBContext;
using DataLayer.Entities;
using Microsoft.Extensions.Options;
using Model;
using Model.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class StockServices : IStockServices
    {
        private readonly IMongoCollection<Stock> _stockCollection;
  
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        public StockServices(IOptions<MongoDBSettings> mongoDBSettings, IMapper mapper, IDbContext dbContext)
        {
          
            _mapper = mapper;
            _dbContext = dbContext;
            _stockCollection = _dbContext.GetCollection<Stock>(mongoDBSettings.Value.CollectionName);

        }
        public async Task<List<StockRes>> Get(string companyCode,DateTime startDate,DateTime endDate)
        {

            var filter = Builders<Stock>.Filter.Gte(x => x.CreatedDate, startDate)
                 & Builders<Stock>.Filter.Lte(x => x.CreatedDate, endDate)
                & Builders<Stock>.Filter.Eq(x => x.CompanyId, companyCode)
                 ;
            var stocks = await _stockCollection.Find(filter).SortByDescending(x => x.CreatedDate).ToListAsync();
            var data = stocks.Select(x => new StockRes { Price = x.Price, CreatedDate = x.CreatedDate }).ToList();
            return data;
        }

        public async Task Create(string id, StockPriceReq stock)
        {
            var stock1 = _mapper.Map<Stock>(stock);
            stock1.CreatedDate = DateTime.Now;
            stock1.CompanyId = id;
            await _stockCollection.InsertOneAsync(stock1);
        }

        public async Task<List<StockRes>> GetALL()
        {
            
            var data = _stockCollection.AsQueryable().Select(x => new StockRes { Price = x.Price, CreatedDate = x.CreatedDate }).OrderByDescending(x=>x.CreatedDate).ToList();
            return data;
        }
    }

}


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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class CompanyServices :  ICompanyServices
    {
        private readonly IMongoCollection<Company> _companyCollection;
        private readonly IMongoCollection<Stock> _stockCollection;
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        public CompanyServices(IOptions<MongoDBSettings> mongoDBSettings, IMapper mapper, IDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _companyCollection = _dbContext.GetCollection<Company>(mongoDBSettings.Value.CollectionName);
            _stockCollection = _dbContext.GetCollection<Stock>("Stock");
            
        }

        //public async Task<List<CompanyReq>> Get()
        //{

        //    var data = await _companyCollection.Find(new BsonDocument()).ToListAsync();
        //    var companies = data.Select(x => new CompanyReq { ComapanyName = x.ComapanyName,
        //        CompanyCEO = x.CompanyCEO, CompanyTurnover = x.CompanyTurnover,
        //        CompanyWebsite = x.CompanyWebsite, Exchange = x.Exchange, CompanyCode = x.CompanyCode }).ToList();
        //    foreach (var item in companies)
        //    {
        //        FilterDefinition<Stock> filter = Builders<Stock>.Filter.Eq("CompanyId", item.CompanyCode);
        //        item.Price = _stockCollection.Find(filter).SortByDescending(x=>x.CreatedDate).FirstOrDefault()?.Price??0;
        //    }

        //    return companies;

        //}
        //public async Task<CompanyReq> Get(string id)
        
        //{
        //    var data = await _companyCollection.Find(x=>x.CompanyCode.Equals(id)).FirstOrDefaultAsync();
            
        //    var com = _mapper.Map<CompanyReq>(data);
        //    FilterDefinition<Stock> filter = Builders<Stock>.Filter.Eq("CompanyId", id);
        //    com.Price = _stockCollection.Find(filter).SortByDescending(x => x.CreatedDate).FirstOrDefault()?.Price??0;
        //    return com;

        //}
        public async Task RegisterCompany(CompanyReq company)
        {
            var com = _mapper.Map<Company>(company);
            await _companyCollection.InsertOneAsync(com);
        }

        public async Task DeleteCompany(string id)
        {
            FilterDefinition<Company> filter = Builders<Company>.Filter.Eq("CompanyCode", id);
            FilterDefinition<Stock> stockFilter = Builders<Stock>.Filter.Eq("CompanyId", id);
            await _companyCollection.DeleteOneAsync(filter);
            await _stockCollection.DeleteManyAsync(stockFilter);
            
        }

        public async Task<List<CompanyRes>> GetAllCompanies()
        {
            var data = await _companyCollection.Find(new BsonDocument()).ToListAsync();
            var companies = data.Select(x => new CompanyRes
            {
                ComapanyName = x.ComapanyName,
                CompanyCEO = x.CompanyCEO,
                CompanyTurnover = x.CompanyTurnover,
                CompanyWebsite = x.CompanyWebsite,
                Exchange = x.Exchange,
                CompanyCode = x.CompanyCode
            }).ToList();
            foreach (var item in companies)
            {
              

                item.Stocks = _stockCollection.AsQueryable().Where(x => x.CompanyId.Equals(item.CompanyCode)).OrderByDescending(x => x.CreatedDate).Select(x => new StockRes { Price = x.Price, CreatedDate = x.CreatedDate }).ToList();

            }

            return companies;
        }

        public async Task<CompanyRes> GetCompaniesById(string id)
        {
            {
                CompanyRes companyRes = new CompanyRes();
                var company = await _companyCollection.Find(x => x.CompanyCode.Equals(id)).FirstOrDefaultAsync();
                if (company != null)
                {
                    companyRes = _mapper.Map<CompanyRes>(company);
                    //FilterDefinition<Stock> filter = Builders<Stock>.Filter.Eq("CompanyId", id);
                    //data.Stocks = _stockCollection.Find(filter).SortByDescending(x => x.CreatedDate.FirstOrDefault();
                    companyRes.Stocks = _stockCollection.AsQueryable().Where(x => x.CompanyId.Equals(id)).OrderByDescending(x => x.CreatedDate).Select(x => new StockRes { Price = x.Price, CreatedDate = x.CreatedDate }).ToList();
                    
                }
                return companyRes;

            }
        }

        public List<CompanyDropdownRes> GetAllCompanyForDropdown()
        {            
            var company =  _companyCollection.AsQueryable().Select(x => new CompanyDropdownRes { Id = x.CompanyCode, Name = x.ComapanyName }).ToList();

            return company;
        }
    }
   
}

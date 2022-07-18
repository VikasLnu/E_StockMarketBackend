using DataLayer.Entities;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
   public interface IStockServices //: IBaseRepository<Stock>
    {

        public Task<List<StockRes>> Get(string companyCode, DateTime startDate, DateTime endDate);
        public Task<List<StockRes>> GetALL();
        public Task Create(string id , StockPriceReq stock);


    }
}

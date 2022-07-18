using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class CompanyRes : CompanyReq
    {
        public string CompanyCode { get; set; }
        public List<StockRes> Stocks { get; set; }
    }
}

using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class StockRes : StockPriceReq
    {     
        public DateTime CreatedDate { get; set; }
    }
}

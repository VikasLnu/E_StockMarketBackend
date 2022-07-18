using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
   public class StockPriceReq 
    {
        [Required]
        public int Price { get; set; }
    }
}

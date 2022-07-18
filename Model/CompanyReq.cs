using Model.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class CompanyReq 
    {
        [Required]
        public string ComapanyName { get; set; }
        [Required]
        public string CompanyCEO { get; set; }
        [Required]
        public string CompanyTurnover { get; set; }
        [Required]
        public string CompanyWebsite { get; set; }
        [Required]
        public string Exchange { get; set; }
        

    }
}

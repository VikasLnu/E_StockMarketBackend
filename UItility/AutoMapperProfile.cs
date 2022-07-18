using AutoMapper;
using DataLayer.Entities;
using Model;
using System;
using System.Collections.Generic;

namespace UItility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CompanyReq,Company>();
            CreateMap<Company,CompanyReq>();
            CreateMap<Company, CompanyRes>();
            CreateMap<StockRes,Stock>();
        }
    }
}

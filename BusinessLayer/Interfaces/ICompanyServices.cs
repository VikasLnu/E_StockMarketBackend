using DataLayer.Entities;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
   public interface ICompanyServices
    {
        public Task<List<CompanyRes>> GetAllCompanies();
        public Task<CompanyRes> GetCompaniesById(string id);
        public Task RegisterCompany(CompanyReq company);
        public Task DeleteCompany(string id);
        public List<CompanyDropdownRes> GetAllCompanyForDropdown();
    }
}

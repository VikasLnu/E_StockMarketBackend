using AutoMapper;
using BusinessLayer.Interfaces;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICompanyServices _companyRepository;
        public HomeController(ICompanyServices companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
        }
        // GET: api/<HomeController>
        [HttpGet]
        public async Task<IActionResult>Get()
        {            
                var data = await _companyRepository.GetAllCompanies();
                return Ok(data);          
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {           
                var data = await _companyRepository.GetCompaniesById(id);
                return Ok(data);          
        }

        // POST api/<HomeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyReq company)
        {          
                await _companyRepository.RegisterCompany(company);
                return Ok();           
        }

        
        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public  IActionResult Delete(string id)
        {            
                 _companyRepository.DeleteCompany(id);
                return Ok();           
        }

        [HttpGet]
        [Route("companieslist")]
        public IActionResult CompaniesList()
        {
           var data = _companyRepository.GetAllCompanyForDropdown();
            return Ok(data);
        }
    }
}

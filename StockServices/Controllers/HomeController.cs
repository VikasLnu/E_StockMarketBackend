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

namespace StockServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStockServices _stockRepository;
        private readonly IMapper _mapper;
        public HomeController(IStockServices stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _stockRepository.GetALL();
            return Ok(data);
        }


        [HttpGet("{id}/{startdate}/{enddate}")]
        public async Task<IActionResult> Get(string id,DateTime startdate, DateTime enddate)
        {
          var data  = await _stockRepository.Get(id, startdate, enddate);
            return Ok(data);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id, [FromBody] StockRes stock)
        {
            await _stockRepository.Create(id,stock);
            return Ok();
        }

    }
}

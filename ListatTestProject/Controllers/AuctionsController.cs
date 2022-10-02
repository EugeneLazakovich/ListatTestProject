using ListatTestProject_BL.DTOs;
using ListatTestProject_BL.Services.SaleService;
using ListatTestProject_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListatTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuctionsController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public AuctionsController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IEnumerable<SaleDto>> GetAll()
        {
            return await _saleService.GetAllSales();
        }

        [HttpGet("{id}")]
        public async Task<SaleDto> GetById(int id)
        {
            return await _saleService.GetByIdSale(id);
        }

        [HttpPost("add")]
        public async Task<int> Add(Sale sale)
        {
            return await _saleService.AddSale(sale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Sale sale)
        {
            try
            {
                sale.Id = id;
                var result = await _saleService.UpdateSale(sale);

                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _saleService.DeleteByIdSale(id);
        }
    }
}

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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("0.1")]
    public class AuctionsController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public AuctionsController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<SaleDto>> GetAll()
        {
            return await _saleService.GetAllSales();
        }

        [HttpGet("{id}")]
        public async Task<SaleDto> GetById(int id)
        {
            return await _saleService.GetByIdSale(id);
        }

        [HttpPost]
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _saleService.DeleteByIdSale(id);
        }

        [HttpGet]
        public async Task<IEnumerable<SaleDto>> GetSaleDtosByFilter(
            [FromQuery] string name,
            [FromQuery] DateTime createdDt,
            [FromQuery] DateTime finishedDt,
            [FromQuery] decimal price,
            [FromQuery] string status,
            [FromQuery] string seller,
            [FromQuery] string buyer,
            [FromQuery] string sort_order,
            [FromQuery] string sort_key,
            [FromQuery] int limit)
        {
            DateTime? createdDtNullable = createdDt.ToString("dd.MM.yyyy") == "01.01.0001" ? null : createdDt;
            DateTime? finishedDtNullable = finishedDt.ToString("dd.MM.yyyy") == "01.01.0001" ? null : finishedDt;
            decimal? priceNullable = price == 0 ? null : price;
            MarketStatus? statusNullable = string.IsNullOrEmpty(status) ? null : ParseStringToMarketStatus(status);
            return await _saleService.GetSalesByFilter(name, createdDtNullable, finishedDtNullable, priceNullable, statusNullable, seller, buyer, sort_order, sort_key, limit);
        }

        private MarketStatus? ParseStringToMarketStatus(string status)
        {
            switch (status.ToLower())
            {
                case "none":
                    return MarketStatus.None;
                case "canceled":
                    return MarketStatus.Canceled;
                case "finished":
                    return MarketStatus.Finished;
                case "active":
                    return MarketStatus.Active;
                default:
                    return null;
            }
        }
    }
}

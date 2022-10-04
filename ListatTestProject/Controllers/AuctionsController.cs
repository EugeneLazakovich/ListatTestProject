using ListatTestProject.Models;
using ListatTestProject_BL.DTOs;
using ListatTestProject_BL.Services.SaleService;
using ListatTestProject_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Add(Sale sale)
        {
            try
            {
                var result = await _saleService.AddSale(sale);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
        public async Task<IActionResult> GetSaleDtosByFilter(
            [FromQuery] string name,
            [FromQuery] string status,
            [FromQuery] string seller,
            [FromQuery] string sort_order,
            [FromQuery] string sort_key,
            [FromQuery] int limit,
            [FromQuery] int page = 1)
        {
            limit = limit != 0 ? limit : 10;
            MarketStatus? statusNullable = string.IsNullOrEmpty(status) ? null : ParseStringToMarketStatus(status);
            var salesDto = await _saleService.GetSalesByFilter(name, statusNullable, seller, sort_order, sort_key, limit, page);
            PageInfo pageInfo = new PageInfo 
            { 
                PageNumber = page, 
                PageSize = limit, 
                TotalItems = salesDto.Count() 
            };
            Response.Headers.Add("PageNumber", pageInfo.PageNumber.ToString());
            Response.Headers.Add("PageSize", pageInfo.PageSize.ToString());
            Response.Headers.Add("TotalItems", pageInfo.TotalItems.ToString());
            Response.Headers.Add("TotalPages", pageInfo.TotalPages.ToString());

            return Ok(salesDto);
        }

        private static MarketStatus? ParseStringToMarketStatus(string status)
        {
            return (status.ToLower()) switch
            {
                "none" => MarketStatus.None,
                "canceled" => MarketStatus.Canceled,
                "finished" => MarketStatus.Finished,
                "active" => MarketStatus.Active,
                _ => null,
            };
        }
    }
}

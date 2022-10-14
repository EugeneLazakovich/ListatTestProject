using ListatTestProject.Models;
using ListatTestProject_BL.DTOs;
using ListatTestProject_BL.Services.SaleService;
using ListatTestProject_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ListatTestProject.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("0.1")]
    [ResponseCache(Duration = 50, VaryByQueryKeys = new string[] { "name", "seller", "status", "sort_order", "sort_key", "limit", "page"})]
    public class AuctionsController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly static CancellationTokenSource cts = new CancellationTokenSource();
        private readonly CancellationToken ct = cts.Token;

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
            [FromQuery] string limit,
            [FromQuery] string page)
        {            
            try
            {
                await Task.Delay(4000, ct);

                if (!Int32.TryParse(limit, out int limitInt))
                {
                    limitInt = 10;
                }

                if (!Int32.TryParse(page, out int pageInt))
                {
                    pageInt = 1;
                }

                MarketStatus? statusNullable = string.IsNullOrEmpty(status) ? null : ParseStringToMarketStatus(status);
                var salesDto = await _saleService.GetSalesByFilter(name, statusNullable, seller, sort_order, sort_key, limitInt, pageInt);
                PageInfo pageInfo = new PageInfo 
                { 
                    PageNumber = pageInt, 
                    PageSize = limitInt, 
                    TotalItems = salesDto.Count()
                };
                Response.Headers.Add("PageNumber", pageInfo.PageNumber.ToString());
                Response.Headers.Add("PageSize", pageInfo.PageSize.ToString());
                Response.Headers.Add("TotalItems", pageInfo.TotalItems.ToString());
                Response.Headers.Add("TotalPages", pageInfo.TotalPages.ToString());

                return Ok(salesDto);
            }
            catch (OperationCanceledException ex)
            {
                return NotFound(ex.Message);
            }            
        }

        [HttpGet("cancel")]
        public IActionResult CancelTask()
        {
            cts.Cancel();

            return Ok();
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

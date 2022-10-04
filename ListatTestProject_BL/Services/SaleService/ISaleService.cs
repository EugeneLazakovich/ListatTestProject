using ListatTestProject_BL.DTOs;
using ListatTestProject_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListatTestProject_BL.Services.SaleService
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDto>> GetAllSales();
        Task<SaleDto> GetByIdSale(int id);
        Task<bool> DeleteByIdSale(int id);
        Task<bool> UpdateSale(Sale sale);
        Task<int> AddSale(Sale sale);
        Task<IEnumerable<SaleDto>> GetSalesByFilter(
            string name,
            MarketStatus? status,
            string seller,
            string sort_order,
            string sort_key);
    }
}

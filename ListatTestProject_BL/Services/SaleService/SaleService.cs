using ListatTestProject_BL.DTOs;
using ListatTestProject_DAL.Interfaces;
using ListatTestProject_DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListatTestProject_BL.Services.SaleService
{
    public class SaleService : ISaleService
    {
        private readonly IGenericRepository<Sale> _saleRepository;
        private readonly IGenericRepository<Item> _itemRepository;

        public SaleService(
            IGenericRepository<Sale> saleRepository,
            IGenericRepository<Item> itemRepository)
        {
            _saleRepository = saleRepository;
            _itemRepository = itemRepository;
        }

        public async Task<int> AddSale(Sale sale)
        {
            return await _saleRepository.Add(sale);
        }

        public async Task<bool> DeleteByIdSale(int id)
        {
            return await _saleRepository.DeleteById(id);
        }

        public async Task<IEnumerable<SaleDto>> GetAllSales()
        {
            return (await _saleRepository.GetAll()).Select(s => MapToSaleDto(s));
        }        

        public async Task<SaleDto> GetByIdSale(int id)
        {
            return MapToSaleDto(await _saleRepository.GetById(id));
        }

        public async Task<bool> UpdateSale(Sale sale)
        {
            return await _saleRepository.Update(sale);
        }

        private SaleDto MapToSaleDto(Sale sale)
        {
            return new SaleDto
            {
                Name = sale.Item.Name,
                CreatedDt = sale.CreatedDt,
                FinishedDt = sale.FinishedDt,
                Price = sale.Price,
                Status = sale.Status.ToString(),
                Seller = sale.Seller,
                Buyer = sale.Buyer
            };
        }
    }
}

using ListatTestProject_BL.DTOs;
using ListatTestProject_DAL.Interfaces;
using ListatTestProject_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListatTestProject_BL.Services.SaleService
{
    public class SaleService : ISaleService
    {
        private readonly IGenericRepository<Sale> _saleGenericRepository;
        private readonly IGenericRepository<Item> _itemGenericRepository;
        private readonly ISaleRepository _saleRepository;

        public SaleService(
            IGenericRepository<Sale> saleGenericRepository,
            IGenericRepository<Item> itemGenericRepository,
            ISaleRepository saleRepository)
        {
            _saleGenericRepository = saleGenericRepository;
            _itemGenericRepository = itemGenericRepository;
            _saleRepository = saleRepository;
        }

        public async Task<int> AddSale(Sale sale)
        {
            var item = await _itemGenericRepository.GetById(sale.ItemId);
            CheckItemOnNull(item);
            sale.Item = item;
            sale.CreatedDt = DateTime.Now;
            return await _saleGenericRepository.Add(sale);
        }

        public async Task<bool> DeleteByIdSale(int id)
        {
            return await _saleGenericRepository.DeleteById(id);
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
            return await _saleGenericRepository.Update(sale);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByFilter(
            string name,
            MarketStatus? status,
            string seller,
            string sort_order,
            string sort_key,
            int limit,
            int page)
        {
            sort_key = string.IsNullOrEmpty(sort_key) ? default : string.Concat(sort_key[0].ToString().ToUpper(), sort_key.ToLower().AsSpan(1));

            var sales = await _saleRepository
                .GetAllByPredicate(name, status, seller, limit, page, sort_key, sort_order);

            return sales.Select(s => MapToSaleDto(s));
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

        private void CheckItemOnNull(Item item)
        {
            if (item == null)
            {
                throw new ArgumentException("The item doesn't exist!");
            }
        }
    }
}

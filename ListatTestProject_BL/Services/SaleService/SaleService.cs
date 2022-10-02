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
            DateTime? createdDt,
            DateTime? finishedDt,
            decimal? price,
            MarketStatus? status,
            string seller,
            string buyer,
            string sort_order,
            string sort_key,
            int limit)
        {
            var sales = await _saleRepository
                .GetAllByPredicate(s => 
                    (string.IsNullOrEmpty(name) || name.ToLower() == s.Item.Name) &&
                    (!createdDt.HasValue || createdDt.Value.Date == s.CreatedDt.Date) &&
                    (!finishedDt.HasValue || finishedDt.Value.Date == s.FinishedDt.Value.Date) &&
                    (!price.HasValue || price.Value == s.Price) &&
                    (!status.HasValue || status.Value == s.Status) &&
                    (string.IsNullOrEmpty(seller) || seller.ToLower() == s.Seller) &&
                    (string.IsNullOrEmpty(buyer) || buyer.ToLower() == s.Buyer));
            if(!string.IsNullOrEmpty(sort_order) && sort_order.ToLower() == "desc")
            {
                if(!string.IsNullOrEmpty(sort_key) && sort_key.ToLower() == "createddt")
                {
                    sales = sales.OrderByDescending(s => s.CreatedDt);
                }
                else if (!string.IsNullOrEmpty(sort_key) && sort_key.ToLower() == "price")
                {
                    sales = sales.OrderByDescending(s => s.Price);
                }
                else
                {
                    sales = sales.OrderByDescending(s => s.Id);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(sort_key) && sort_key.ToLower() == "createddt")
                {
                    sales = sales.OrderBy(s => s.CreatedDt);
                }
                if (!string.IsNullOrEmpty(sort_key) && sort_key.ToLower() == "price")
                {
                    sales = sales.OrderBy(s => s.Price);
                }
            }

            limit = limit == 0 ? 10 : limit;

            return sales.Take(limit).Select(s => MapToSaleDto(s));
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

using ListatTestProject_DAL.Interfaces;
using ListatTestProject_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListatTestProject_BL.Services.SaleService
{
    public class SaleService : ISaleService
    {
        private readonly IGenericRepository<Sale> _saleRepository;

        public SaleService(IGenericRepository<Sale> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<int> AddSale(Sale sale)
        {
            return await _saleRepository.Add(sale);
        }

        public async Task<bool> DeleteByIdSale(int id)
        {
            return await _saleRepository.DeleteById(id);
        }

        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await _saleRepository.GetAll();
        }

        public async Task<Sale> GetByIdSale(int id)
        {
            return await _saleRepository.GetById(id);
        }

        public async Task<bool> UpdateSale(Sale sale)
        {
            return await _saleRepository.Update(sale);
        }
    }
}

using ListatTestProject_DAL.Interfaces;
using ListatTestProject_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListatTestProject_BL.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IGenericRepository<Item> _itemRepository;

        public ItemService(IGenericRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<int> AddItem(Item item)
        {
            return await _itemRepository.Add(item);
        }

        public async Task<bool> DeleteByIdItem(int id)
        {
            return await _itemRepository.DeleteById(id);
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _itemRepository.GetAll();
        }

        public async Task<Item> GetByIdItem(int id)
        {
            return await _itemRepository.GetById(id);
        }

        public async Task<bool> UpdateItem(Item item)
        {
            return await _itemRepository.Update(item);
        }
    }
}

using ListatTestProject_DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListatTestProject_BL.Services.ItemService
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task<Item> GetByIdItem(int id);
        Task<bool> DeleteByIdItem(int id);
        Task<bool> UpdateItem(Item item);
        Task<int> AddItem(Item item);
    }
}

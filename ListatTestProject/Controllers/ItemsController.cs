using ListatTestProject_BL.Services.ItemService;
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
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _itemService.GetAllItems();
        }

        [HttpGet("{id}")]
        public async Task<Item> GetById(int id)
        {
            return await _itemService.GetByIdItem(id);
        }

        [HttpPost("add")]
        public async Task<int> Add(Item item)
        {
            return await _itemService.AddItem(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {
            try
            {
                item.Id = id;
                var result = await _itemService.UpdateItem(item);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("delete/{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _itemService.DeleteByIdItem(id);
        }
    }
}

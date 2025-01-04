using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var item = await _itemService.GetItems();
            var totalItemCount =  await _itemService.GetTotalItemCount();

            var response = new ItemResponceDto
            {
               totalCount = totalItemCount,
               items = item

            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _itemService.GetItemById(id);
            if(item is null) 
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] ItemDto itemDto)
        {
            await _itemService.AddItem(itemDto);
            return CreatedAtAction(nameof(AddItem), new { id = itemDto.ItemId });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemDto itemDto)
        {
            if (id != itemDto.ItemId) return BadRequest();
            await _itemService.UpdateItem(itemDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DetelteItem(int id)
        {
            var item = await _itemService.GetItemById(id);
            if (item is null) 
                return NotFound();

            await _itemService.DeleteItem(id);
            return NoContent();
        }

    }
}

using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Services
{
    public class ItemService: IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;
        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger) 
        {
          _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            _logger.LogInformation("Getting All Items");
            try
            {
                var item = await _itemRepository.GetItems();
                return item.Select(item => new ItemDto
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    ReorderLevel = item.ReorderLevel
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while getting items");
                throw;
            }
        }

        public async Task<ItemDto> GetItemById(int Id)
        {
            _logger.LogInformation("Getting Item by Id: {ItemId}",Id);
            try
            {
                var item = await _itemRepository.GetItemById(Id);
                if (item is null)
                {
                    return null;
                }

                return new ItemDto
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    ReorderLevel = item.ReorderLevel
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting item by Id: {ItemId}",Id);
                throw;
            }
        }

        public async Task AddItem(ItemDto itemDto)
        {
            _logger.LogInformation("Adding new item: {ItemName}",itemDto.Name);
            try
            {
                var item = new Item
                {
                    ItemId = itemDto.ItemId,
                    Name = itemDto.Name,


                    Description = itemDto.Description,
                    Quantity = itemDto.Quantity,
                    ReorderLevel = itemDto.ReorderLevel
                };
                await _itemRepository.AddItem(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Adding item: {ItemName}", itemDto.Name);
                throw;
            }
        }

        public async Task UpdateItem(ItemDto itemDto)
        {
            _logger.LogInformation("Updating new item: {ItemName}", itemDto.Name);
             try
            {
                 var item = new Item
                 {
                   ItemId=itemDto.ItemId,
                   Name = itemDto.Name,
                   Description = itemDto.Description,
                   Quantity = itemDto.Quantity,
                   ReorderLevel = itemDto.ReorderLevel
                 };
                await _itemRepository.UpdateItem(item);
             }
              catch (Exception ex)
              {
                _logger.LogError(ex, "Error occurred while Updating item: {ItemName}", itemDto.Name);
                throw;
              }
        }
        public async Task DeleteItem(int Id)
        {
            _logger.LogInformation("Deleting new item: {ItemId}", Id);
            try
            {
                await _itemRepository.DeleteItem(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Updating item: {ItemId}", Id);
                throw;
            }
        }
        public async Task<int> GetTotalItemCount()
        {
            _logger.LogInformation("Getting Total Items Count ");
            try
            {
                var item = await _itemRepository.GetItems();
                return item.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Err" +
                    "" +
                    "or occurred while Getting Total Items Count");
                throw;
            }
        }
    }
}

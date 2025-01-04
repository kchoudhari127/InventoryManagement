using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
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
        public ItemService(IItemRepository itemRepository) 
        {
          _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<ItemDto>> GetItems()
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

        public async Task<ItemDto> GetItemById(int Id)
        {
           var item = await _itemRepository.GetItemById(Id);
            if(item is null)
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

        public async Task AddItem(ItemDto itemDto)
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

        public async Task UpdateItem(ItemDto itemDto)
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
        public async Task DeleteItem(int Id)
        {
             await _itemRepository.DeleteItem(Id);
        }
        public async Task<int> GetTotalItemCount()
        {
            var item = await _itemRepository.GetItems();
            return item.Count();
        }
    }
}

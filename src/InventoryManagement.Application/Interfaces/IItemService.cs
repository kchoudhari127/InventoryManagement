using InventoryManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetItems();
        Task<ItemDto> GetItemById(int Id);
        Task AddItem(ItemDto itemDto);
       Task UpdateItem(ItemDto itemDto);
        Task DeleteItem(int Id);
        Task<int> GetTotalItemCount();
    }
}

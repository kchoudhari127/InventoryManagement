﻿using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItems();
        Task<Item> GetItemById(int Id);
        Task AddItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItem(int Id);
    }
}

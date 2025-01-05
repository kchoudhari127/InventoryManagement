using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Domain.Interfaces;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ItemRepository: IItemRepository
    {
        private readonly ApplicationDbContext _context; 
        public ItemRepository(ApplicationDbContext context) 
        { 
            _context = context; 
        }
        public async Task<IEnumerable<Item>> GetItems() 
        { 
            return await _context.Items.ToListAsync(); 
        }

        public async Task<Item> GetItemById(int Id)
        {
            return await _context.Items.FindAsync(Id);
        }
        public async Task AddItem(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItem(Item item)
        {
             _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(int Id)
        {
            var item = await _context.Items.FindAsync(Id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}

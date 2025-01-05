using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext) 
        {
           _context = dbContext;
        }

        public async Task<User> Authenticate(string usernme ,string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == usernme && u.Password == password);

        }
    }
}

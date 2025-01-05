using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<(string token, DateTime expires)> GererateToken(string username, string role);
    }
}

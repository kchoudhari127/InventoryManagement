using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int ExpiresIn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs
{
    public class ItemResponceDto
    {
        public int totalCount { get; set; }
        public IEnumerable<ItemDto> items { get; set; }
    }
}

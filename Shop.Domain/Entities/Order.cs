using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Order
    {
        public uint OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DateOfPlacing { get; set; } = DateTime.Now;
        public DateTime? DateOfCompletion { get; set; }
        public string? OrderProducts { get; set; } = "";
    }
}

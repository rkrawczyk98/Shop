using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Core.Models
{
    public class Order
    {
        public decimal Discount { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }


        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

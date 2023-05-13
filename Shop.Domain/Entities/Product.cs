using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        //public string CategoryCategory { get; set; }
        public string Price { get; set; }
        public string? AuthorId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get;set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}

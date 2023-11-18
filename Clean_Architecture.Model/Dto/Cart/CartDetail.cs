using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Dto.Cart
{
    public class CartDetail
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
    }
}

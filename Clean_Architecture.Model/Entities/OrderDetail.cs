using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int UserId { get; set; }
        [ForeignKey("Id")]
        public virtual AccountClient AccountClient { get; set; }
        public int OderId { get; set; }
        [ForeignKey("Id")]
        public virtual Order Order { get; set; }
    }
}

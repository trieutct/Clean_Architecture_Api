using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Dto.OrderDetail
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string LinkAnh { get;set; }
        public int OderId { get; set; }
    }
}

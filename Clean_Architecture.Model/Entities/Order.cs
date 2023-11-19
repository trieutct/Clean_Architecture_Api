using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string NguoiNhan { get; set; }
        public string Phone { get; set; }
        public string DiaChi { get; set; }
        public string Total { get; set; }
        public DateTime NgayDat { get; set; }
        public int TrangThai { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}

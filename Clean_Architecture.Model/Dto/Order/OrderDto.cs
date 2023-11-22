using Clean_Architecture.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Dto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string NguoiNhan { get; set; }
        public string Phone { get; set; }
        public string DiaChi { get; set; }
        public long Total { get; set; }
        public DateTime NgayDat { get; set; }
        public int TrangThai { get; set; }
        public int UserId { get; set; }

    }
}

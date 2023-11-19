namespace Clean_Architecture.Api.Controllers.OrderController
{
    public class AddOrder
    {
        public string NguoiNhan { get; set; }
        public string Phone { get; set; }
        public string DiaChi { get; set; }
        public long Total { get; set; }
        public List<int> ListCartId { get; set; }
        public int UserId { get; set; }
    }
}

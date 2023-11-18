namespace Clean_Architecture.Api.Controllers.CartController
{
    public class CartDetail
    {
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
    }
}

namespace Clean_Architecture.Api.Controllers.CartController
{
    public class AddCart
    {
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}

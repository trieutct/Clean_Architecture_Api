namespace Clean_Architecture.Api.Controllers.ProductController
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public IFormFile? file { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string CategoryId { get; set; }
        public string? ProductImage { get; set; }
    }
}

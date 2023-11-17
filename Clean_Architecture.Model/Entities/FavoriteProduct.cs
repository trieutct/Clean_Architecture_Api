using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Entities
{
    public class FavoriteProduct
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("Id")]
        public AccountClient AccountClient { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}

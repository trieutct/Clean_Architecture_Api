using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Entities
{
    public class AccountClient
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual IEnumerable<FavoriteProduct> FavoriteProducts { get; set; }
    }
}

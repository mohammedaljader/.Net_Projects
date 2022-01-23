using System.Collections.Generic;

namespace Models.DataModels
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Product> Cart { get; set; }
        public List<Product> WishList { get; set; }
    }
}
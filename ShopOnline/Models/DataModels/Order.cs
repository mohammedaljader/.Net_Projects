using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataModels
{
    public class Order
    {
        public int ID { get; set; }
        public List<Product> Products { get; set; }
        public User User { get; set; }
        public Billing Billing { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

    }
}

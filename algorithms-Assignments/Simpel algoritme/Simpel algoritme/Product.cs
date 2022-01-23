using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpel_algoritme
{
    public class Product
    {
        //Properties
        public string Name { get; set; }
        public int Price { get; set; }

        //constractor
        public Product(string name, int price)
        {
            this.Name = name;
            this.Price = price;
        }

        //To string methode 
        public override string ToString()
        {
            return $"The name of the product is {Name} and the price is {Price}";
        }
    }
}

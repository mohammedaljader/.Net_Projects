using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpel_algoritme
{
    public class Order
    {
        //list of products 
        private List<Product> products;

        //Constractor
        public Order()
        {
            products = new List<Product>();
        }
        //Methode to add product to the list 
        public void AddTolist(string name, int price)
        {
            Product product = new Product(name, price);
            products.Add(product);
        }
        //Give the maximum price form the list 
        public double GiveMaximumPrice()
        {
            double maxvalue = products.Max(x => x.Price);
            return maxvalue;
        }
        //Give the average of prices 
        public double GiveAveragePrice()
        {
            double average = products.Average(x => x.Price);
            return average;
        }
        //Get All products where the price is the same as the minimunPrice
        public List<Product> GetAllProducts(double minimumPrice)
        {
            //var allproduct = products.Where(x => x.Price == minimumPrice); // (Where way) but i have to use IEnumerable instead of List 
            var allproduct  = products.FindAll(x => x.Price == minimumPrice);
            return allproduct;
        }
        //sorting products by price using (Orderby)
        public void SortProductsByPrice()
        {
            Console.WriteLine("========================");
            foreach (var item in products)
            {
                Console.WriteLine(item.Price);
            }
            Console.WriteLine("========================");
            var SortingByPrice =   products.OrderBy(x=> x.Price);
            foreach (var item in SortingByPrice)
            {
                Console.WriteLine(item.Price);
            }
        }
    }
}

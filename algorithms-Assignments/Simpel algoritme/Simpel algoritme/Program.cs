using System;
using System.Collections.Generic;

namespace Simpel_algoritme
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();
            //Add to list
            order.AddTolist("A1", 1000);
            order.AddTolist("A2", 140);
            order.AddTolist("A3", 13000);
            order.AddTolist("A4", 5000);
            order.AddTolist("A5", 5000);
            order.AddTolist("A6", 19000);

            //Give max price
            double max = order.GiveMaximumPrice();
            Console.WriteLine($"The Maximum price is {max}");
            //Give average price
            double average = order.GiveAveragePrice();
            Console.WriteLine($"The average of the prices is {average}");
            //Get all products  
            var Allproducts = order.GetAllProducts(5000);
            Console.Write($"The products are: ");
            foreach (var item in Allproducts)
            {
                Console.WriteLine(item);
            }
            //Sorting products By price  
            order.SortProductsByPrice();
            Console.ReadLine();
        }
    }
}



using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a product logic class.
    /// </summary>
    public interface IProductLogic
    {
        List<Product> GetAll();
        Product GetProductById(int id);
        void EditProduct(Product product);
        void AddProduct(Product product);
        void RemoveProduct(Product product);
    }
}
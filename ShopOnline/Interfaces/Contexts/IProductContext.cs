using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a product context.
    /// </summary>
    public interface IProductContext
    {
        List<Product> GetAll();
        Product GetProductById(int id);
        void EditProduct(Product product);
        void AddProduct(Product product);
        void RemoveProduct(Product product);
    }
}
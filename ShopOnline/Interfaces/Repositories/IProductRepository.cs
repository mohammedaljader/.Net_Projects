using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a product repository.
    /// </summary>
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetProductById(int id);
        void EditProduct(Product product);
        void AddProduct(Product product);
        void RemoveProduct(Product product);
    }
}
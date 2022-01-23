using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext Context;

        public ProductRepository(IProductContext context)
        {
            Context = context;
        }
        public List<Product> GetAll()
        {
            return Context.GetAll();
        }

        public Product GetProductById(int id)
        {
            return Context.GetProductById(id);
        }

        public void AddProduct(Product product)
        {
            Context.AddProduct(product);
        }

        public void EditProduct(Product product)
        {
            Context.EditProduct(product);
        }

        public void RemoveProduct(Product product)
        {
            Context.RemoveProduct(product);
        }
    }
}
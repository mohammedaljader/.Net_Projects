using Data;
using Data.Contexts;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Logic.LogicObjects
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository productRepository;

        public ProductLogic(IProductContext context)
        {
            productRepository = new ProductRepository(context);
        }

        public List<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public Product GetProductById(int id)
        {
            return productRepository.GetProductById(id);
        }

        public void AddProduct(Product product)
        {
            productRepository.AddProduct(product);
        }

        public void EditProduct(Product product)
        {
            productRepository.EditProduct(product);
        }

        public void RemoveProduct(Product product)
        {
            productRepository.RemoveProduct(product);
        }
    }
}
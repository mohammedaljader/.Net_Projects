using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ICartContext Context;

        public CartRepository(ICartContext context)
        {
            Context = context;
        }

        public List<Product> GetCartByUser(User user)
        {
            return Context.GetCartByUser(user);
        }

        public void AddToCart(Product product, User user)
        {
            Context.AddToCart(product, user);
        }

        public void RemoveFromCart(Product product, User user)
        {
            Context.RemoveFromCart(product, user);
        }
    }
}
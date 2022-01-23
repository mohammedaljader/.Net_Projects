using Data;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Logic.LogicObjects
{
    public class CartLogic : ICartLogic
    {
        private readonly ICartRepository cartRepository;

        public CartLogic(ICartContext context)
        {
            cartRepository = new CartRepository(context);
        }

        public List<Product> GetCartByUser(User user)
        {
            return cartRepository.GetCartByUser(user);
        }

        public void AddToCart(Product product, User user)
        {
            cartRepository.AddToCart(product, user);
        }
        public void RemoveFromCart(Product product, User user)
        {
            cartRepository.RemoveFromCart(product, user);
        }
    }
}
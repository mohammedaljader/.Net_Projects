using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a cart context.
    /// </summary>
    public interface ICartContext
    {
        List<Product> GetCartByUser(User user);
        void AddToCart(Product product, User user);
        void RemoveFromCart(Product product, User user);
    }
}
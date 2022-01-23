

using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a cart logic class.
    /// </summary>
    public interface ICartLogic
    {
        List<Product> GetCartByUser(User user);
        void AddToCart(Product product, User user);
        void RemoveFromCart(Product product, User user);
    }
}
using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a cart repository.
    /// </summary>
    public interface ICartRepository
    {
        List<Product> GetCartByUser(User user);
        void AddToCart(Product product, User user);
        void RemoveFromCart(Product product, User user);
    }
}
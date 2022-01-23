using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a wish context.
    /// </summary>
    public interface IWishContext
    {
        List<Product> GetWishesByUser(User user);
        void AddToWishList(Product product, User user);
        void RemoveFromWishList(Product product, User user);
    }
}
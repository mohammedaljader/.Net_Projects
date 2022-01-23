

using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a wish logic class.
    /// </summary>
    public interface IWishLogic
    {
        List<Product> GetWishesByUser(User user);
        void AddToWishList(Product product, User user);
        void RemoveFromWishList(Product product, User user);
    }
}
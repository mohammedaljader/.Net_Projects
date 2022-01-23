using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a wish repository.
    /// </summary>
    public interface IWishRepository
    {
        List<Product> GetWishesByUser(User user);
        void AddToWishList(Product product, User user);
        void RemoveFromWishList(Product product, User user);
    }
}
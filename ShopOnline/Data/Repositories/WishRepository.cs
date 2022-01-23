using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class WishRepository : IWishRepository
    {
        private readonly IWishContext Context;

        public WishRepository(IWishContext context)
        {
            Context = context;
        }

        public List<Product> GetWishesByUser(User user)
        {
            return Context.GetWishesByUser(user);
        }

        public void AddToWishList(Product product, User user)
        {
            Context.AddToWishList(product, user);
        }

        public void RemoveFromWishList(Product product, User user)
        {
            Context.RemoveFromWishList(product, user);
        }
    }
}
using Data;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Logic.LogicObjects
{
    public class WishLogic : IWishLogic
    {
        private readonly IWishRepository wishRepository;

        public WishLogic(IWishContext context)
        {
            wishRepository = new WishRepository(context);
        }

        public List<Product> GetWishesByUser(User user)
        {
            return wishRepository.GetWishesByUser(user);
        }

        public void AddToWishList(Product product, User user)
        {
            wishRepository.AddToWishList(product, user);
        }

        public void RemoveFromWishList(Product product, User user)
        {
            wishRepository.RemoveFromWishList(product, user);
        }
    }
}
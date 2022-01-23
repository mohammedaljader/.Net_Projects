using Exceptions.Cart;
using Exceptions.User;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Contexts
{
    public class CartMemoryContext : ICartContext
    {

        public List<Product> GetCartByUser(User user)
        {
            return user.Cart;
        }

        public void AddToCart(Product product, User user)
        {
            try
            {

                user.Cart.Add(product);
            }
            catch (Exception)
            {
                throw new AddingToCartFailedException();
            }
        }

        public void RemoveFromCart(Product product, User user)
        {
            try
            {
                user.Cart.RemoveAll(foundProduct => foundProduct.ID == product.ID);
            }
            catch (Exception)
            {
                throw new RemovingFromCartFailedException();
            }
        }
    }
}
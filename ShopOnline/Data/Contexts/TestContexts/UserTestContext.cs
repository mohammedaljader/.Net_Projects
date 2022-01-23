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
    public class UserTestContext : IUserContext
    {
        List<User> users = new List<User>();

        public User GetUserById(int id)
        {
            return users.Find(foundUser => foundUser.ID == id);
        }

        public User Login(string email, string password)
        {
            return users.Find(foundUser => foundUser.Email == email && foundUser.Password == password);
        }

        public void Register(User user)
        {
            users.Add(user);
        }

        public void EditUser(User user)
        {
            users.RemoveAll(foundUser => foundUser.ID == user.ID);
            users.Add(user);
        }
    }
}
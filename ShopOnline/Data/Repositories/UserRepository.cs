using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext Context;

        public User GetUserById(int id)
        {
            return Context.GetUserById(id);
        }

        public UserRepository(IUserContext context)
        {
            Context = context;
        }

        public User Login(string email, string password)
        {
            return Context.Login(email, password);
        }
        public void Register(User user)
        {
            Context.Register(user);
        }

        public void EditUser(User user)
        {
            Context.EditUser(user);
        }
    }
}
using Data;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Exceptions.User;

namespace Logic.LogicObjects
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository UserRepository;

        public UserLogic(IUserContext context)
        {
            UserRepository = new UserRepository(context);
        }

        public User GetUserById(int id)
        {
            return UserRepository.GetUserById(id);
        }

        public User Login(string email, string password)
        {
            if (!new EmailAddressAttribute().IsValid(email))
            {
                throw new AuthenticationFailedException();
            }
            return UserRepository.Login(email, new Hasher().GetSha256FromString(password));
        }

        public void Register(User user)
        {
            if (!new EmailAddressAttribute().IsValid(user.Email))
            {
                throw new RegistrationFailedException();
            }
            user.Password = new Hasher().GetSha256FromString(user.Password);
            UserRepository.Register(user);
        }

        public void EditUser(User user)
        {
            user.Password = new Hasher().GetSha256FromString(user.Password);
            UserRepository.EditUser(user);
        }
    }
}
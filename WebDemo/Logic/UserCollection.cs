using DAL.Factory;
using DAL.interfaces;
using Logic.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserCollection : IUserCollection
    {
        private List<User> Users;
        private readonly IUserDAL UserDAL;


        public UserCollection()
        {
            Users = new List<User>();
            UserDAL = UserDALFactory.UserFactory();
            Users.Add(new User(12111, "", 22, ""));
            foreach (var dto in UserDAL.GetAllUsers())
            {
                Users.Add(new User(dto));
            }
        }

        public void Adduser(string name , int age , string email)
        {
            User user = new User(name, age, email);
            Users.Add(user);
            UserDAL.AddUser(user.ConvertToDto());
        }

        public IReadOnlyCollection<IUser> GetAllUsers()
        {
            Users.Clear();
            List<UserDto> usersDAL = UserDAL.GetAllUsers();
            foreach (var dto in usersDAL)
            {
                Users.Add(new User(dto));
            }
            return Users.AsReadOnly();
        }
    }
}

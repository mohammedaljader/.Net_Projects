using BAL_interfaces;
using DAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public abstract class User : IUser
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }

        public User(int id, string fullname, string username, string password, string email, string telephone, string address)
        {
            Id = id;
            Fullname = fullname;
            Username = username;
            Password = password;
            Email = email;
            Telephone = telephone;
            Address = address;
        }
        public User(string fullname, string username, string password, string email, string telephone, string address)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            Email = email;
            Telephone = telephone;
            Address = address;
        }

        public User(string username , string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public User(UserDto user)
         : this(user.Id, user.Fullname, user.Username ,user.Password, user.Email, user.Telephone , user.Address)
        {

        }

        public void ChangeProfile()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}

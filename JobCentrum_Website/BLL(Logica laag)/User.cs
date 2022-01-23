using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;
using DAL_interfaces.DTO_s;

namespace BLL_Logica_laag_
{
    public abstract class User : IUser
    {


        #region Properties
        public int Id { get; }
        public string Fullname { get; }
        public string Username { get; }
        public string Password { get; }
        public string Email { get; }
        public string Telephone { get; }
        public string Address { get; }
        #endregion

        #region Constractors
        public User()
        {

        }
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
        public User(UserDto user)
         : this(user.Id, user.Fullname, user.Username, user.Password, user.Email, user.Telephone, user.Address)
        {

        }
        #endregion

        #region Methodes
        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void ChangeProfile()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

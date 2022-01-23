using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces
{
    public abstract class UserDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }

        public UserDto(int id, string fullname, string username, string password, string email, string telephone, string address)
        {
            Id = id;
            Fullname = fullname;
            Username = username;
            Password = password;
            Email = email;
            Telephone = telephone;
            Address = address;
        }
    }
}

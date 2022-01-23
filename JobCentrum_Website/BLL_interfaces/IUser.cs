using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface IUser
    {
        public int Id { get; }
        public string Fullname { get; }
        public string Username { get; }
        public string Password { get; }
        public string Email { get; }
        public string Telephone { get; }
        public string Address { get; }

    }
}

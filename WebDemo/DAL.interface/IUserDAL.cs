using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.interfaces
{
    public interface IUserDAL
    {
        void AddUser(UserDto userDto);
        void DeleteUser(int id);
        void EditUser(UserDto userDto);
        List<UserDto> GetAllUsers();
    }
}

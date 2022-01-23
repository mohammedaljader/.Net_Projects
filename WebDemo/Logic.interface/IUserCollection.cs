using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.interfaces
{
    public interface IUserCollection
    {
        void Adduser(string name, int age, string email);

        IReadOnlyCollection<IUser> GetAllUsers();
    }
}

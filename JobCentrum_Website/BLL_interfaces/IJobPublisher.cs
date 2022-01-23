using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface IJobPublisher : IUser
    {
        public int Company_Id { get; }

        public string Company_Name { get; }

        public string Company_Address { get; }

        void ChangeProfileJobPublisher(int id, string fullname, string username, string password, string email, string telephone, string address, int jobPublisher_id, string companyName, string companyAddress);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces
{
    public class CompanyDto : UserDto 
    {
        public int Company_Id { get; set; }

        public string Company_Name { get; set; }

        public string Company_Address { get; set; }

        public CompanyDto (int userId, string fullname, string username, string password, string email, string telephone, string address, int companyId, string companyName, string companyAddress) : base(userId, fullname, username, password, email, telephone, address)
        {
            Company_Id = companyId;
            Company_Name = companyName;
            Company_Address = companyAddress;
        }
    }
}

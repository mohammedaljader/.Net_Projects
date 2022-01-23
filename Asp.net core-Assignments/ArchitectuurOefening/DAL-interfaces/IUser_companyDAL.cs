using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces
{
    public interface IUser_companyDAL
    {
        void Register(CompanyDto company);
        CompanyDto Login(string username, string password);
        void EditProfile(CompanyDto company);
        List<CompanyDto> GetAllJobPublishers();
    }
}

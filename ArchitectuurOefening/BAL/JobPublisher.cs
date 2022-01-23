using BAL_interfaces;
using DAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class JobPublisher : User , IJobPublisher
    {
        public int Company_Id { get; set; }

        public string Company_Name { get; set; }

        public string Company_Address { get; set; }

        public JobPublisher(int userId, string fullname, string username, string password, string email, string telephone, string address, int companyId, string companyName, string companyAddress) : base(userId, fullname, username, password, email, telephone, address)
        {
            Company_Id = companyId;
            Company_Name = companyName;
            Company_Address = companyAddress;
        }
        public JobPublisher(string username, string password) : base(username, password)
        {

        }
        public JobPublisher(CompanyDto user)
            : this(user.Id, user.Fullname, user.Username, user.Password, user.Email, user.Telephone, user.Address, user.Company_Id, user.Company_Name, user.Company_Address)
        {

        }
        public JobPublisher(string fullname, string username, string password, string email, string telephone, string address, string company_name, string company_address) : base(fullname, username, password, email, telephone, address)
        {
            this.Company_Name = company_name;
            this.Company_Address = company_address;
        }

        public CompanyDto ConvertJobPublisherToDto()
        {
            return new CompanyDto(this.Id, this.Fullname, this.Username, this.Password, this.Email, this.Telephone, this.Address, this.Company_Id, this.Company_Name, this.Company_Address);
        }
    }
}

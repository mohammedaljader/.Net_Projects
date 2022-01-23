using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;
using DAL_Factory;
using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;

namespace BLL_Logica_laag_
{
    public class JobPublisher : User , IJobPublisher 
    {
        #region declarations and relations
        private readonly IUser_companyDAL _CompanyDAL;
        #endregion

        #region Properties
        public int Company_Id { get;  }

        public string Company_Name { get; }

        public string Company_Address { get; }
        #endregion

        #region Constractors
        public JobPublisher(int userId, string fullname, string username, string password, string email, string telephone, string address, int companyId, string companyName, string companyAddress) : base(userId, fullname, username, password, email, telephone, address)
        {
            Company_Id = companyId;
            Company_Name = companyName;
            Company_Address = companyAddress;
            _CompanyDAL = AccountDalFactory.User_JobPublisher();
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
        #endregion

        #region Methodes
        public void ChangeProfileJobPublisher(int id, string fullname, string username, string password, string email, string telephone, string address, int jobPublisher_id, string companyName, string companyAddress)
        {
            JobPublisher jobPublisher = new JobPublisher(id, fullname, username, password, email, telephone, address, jobPublisher_id, companyName, companyAddress);
            _CompanyDAL.EditProfile(jobPublisher.ConvertJobPublisherToDto());
        }
        #endregion
    }
}

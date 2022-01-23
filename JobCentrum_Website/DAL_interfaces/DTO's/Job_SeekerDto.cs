using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.DTO_s
{
    public class Job_SeekerDto : UserDto
    {
        public int Seeker_Id { get; set; }
        public int Experience { get; set; }
        public string Hobby { get; set; }

        //public UserDto User_Id { get; set; }

        public Job_SeekerDto(int userId, string fullname, string username, string password, string email, string telephone, string address, int seekerId, int experience, string hobby) : base(userId, fullname, username, password, email, telephone, address)
        {
            Seeker_Id = seekerId;
            Experience = experience;
            Hobby = hobby;
        }
    }
}

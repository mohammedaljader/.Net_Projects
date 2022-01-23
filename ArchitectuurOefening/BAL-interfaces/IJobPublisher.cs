using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_interfaces
{
    public interface IJobPublisher : IUser
    {
        public int Company_Id { get; set; }
        public string Company_Name { get; set; }
        public string Company_Address { get; set; }
    }
}

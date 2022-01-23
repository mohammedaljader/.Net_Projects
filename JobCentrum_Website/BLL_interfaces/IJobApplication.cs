using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface IJobApplication
    {
        int Apply_Id { get; }
        string Motivation { get; }
        string CV { get; }
        DateTime Apply_date { get; }
        int Job_id { get; }
        int JobSeeker_id { get; }
    }
}

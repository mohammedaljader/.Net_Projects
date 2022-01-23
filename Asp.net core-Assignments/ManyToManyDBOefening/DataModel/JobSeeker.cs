using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class JobSeeker
    {
        public int JobSeeker_ID { get; set; }
        public User User { get; set; }
        public int Experience { get; set; }
        public string Hobby { get; set; }
    }
}

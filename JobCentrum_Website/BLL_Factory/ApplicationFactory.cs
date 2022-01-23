using BLL_interfaces;
using BLL_Logica_laag_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Factory
{
    public static class ApplicationFactory
    {
        public static IJobSeeker Application()
        {
            return new JobSeeker();
        }
    }
}

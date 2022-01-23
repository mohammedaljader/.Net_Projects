using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DBConnection
    {
        public static string ConnectionString { get; private set; } = @"Server=studmysql01.fhict.local;Uid=dbi461166;Database=dbi461166;Pwd=fontys;";
    }
}

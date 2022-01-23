using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDBConnection
    {
        public static string ConnectionString { get; set; }
    }
}

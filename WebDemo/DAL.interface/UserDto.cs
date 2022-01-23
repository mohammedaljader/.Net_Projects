using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.interfaces
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public UserDto(int id , string name, int age, string email)
        {
            ID = id;
            Name = name;
            Age = age;
            Email = email;
        }
    }
}

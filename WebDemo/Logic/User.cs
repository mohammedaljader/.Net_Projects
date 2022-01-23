using DAL.interfaces;
using Logic.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class User : IUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public User(int id, string name, int age, string email)
        {
            ID = id;
            Name = name;
            Age = age;
            Email = email;
        }

        public User(string name, int age, string email)
        {
            Name = name;
            Age = age;
            Email = email;
        }

        //Dto naar user
        public User(UserDto userDto): this(userDto.ID, userDto.Name, userDto.Age, userDto.Email) { }

        //user naar dto
        public UserDto ConvertToDto()
        {
            return new UserDto(this.ID, this.Name, this.Age, this.Email);
        }
    }
}

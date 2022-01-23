using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Jij moet jouw naam schrijven")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Je moet je age geven")]
        [Display(Name = "Leeftijd")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Jij moet je email addres geven")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

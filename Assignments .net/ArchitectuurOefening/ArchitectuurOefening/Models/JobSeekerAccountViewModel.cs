using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectuurOefening.Models
{
    public class JobSeekerAccountViewModel
    {
        //[Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "You need to give us your full name.")]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "You need to give us your username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "You need to give us your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "You need to give us your confirm password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirm password must match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = " You need to give us your E-mail address.")]
        [Display(Name = "E-mail Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "You need to give us your Phone number.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "You need to give us your address.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Key]
        public int Seeker_Id { get; set; }
        [Required(ErrorMessage = "You need to give us your Experience.")]
        [Range(1, 100, ErrorMessage = "Experience must not be 0 or bigger than 100")]
        public int Experience { get; set; }
        [Required(ErrorMessage = "You need to give us your Hobby.")]
        public string Hobby { get; set; }
    }
}

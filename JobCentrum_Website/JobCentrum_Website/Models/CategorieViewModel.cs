using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobCentrum_Website.Models
{
    public class CategorieViewModel
    {
        [Required]
        [Display(Name = "Categorie ID")]
        public int Categorie_Id { get; set; }
        [Required]
        [Display(Name = "Categorie Name")]
        public string Categorie_name { get; set; }
        [Required]
        [Display(Name = "Categorie Description")]
        public string Categorie_description { get; set; }

        public IEnumerable<JobViewModel> Jobs { get; set; }
    }
}

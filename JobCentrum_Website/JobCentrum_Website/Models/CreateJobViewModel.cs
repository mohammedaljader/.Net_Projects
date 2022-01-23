using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobCentrum_Website.Models
{
    public class CreateJobViewModel
    {
        [Required(ErrorMessage = "You need to give us the job name.")]
        [Display(Name = "Job name")]
        public string Job_name { get; set; }
        [Required(ErrorMessage = "You need to give us the job description.")]
        [Display(Name = "Job Description")]
        [DataType(DataType.MultilineText)]
        public string Job_description { get; set; }
        [Required(ErrorMessage = "You need to give us the job image.")]
        [Display(Name = "Job image")]
        [DataType(DataType.Upload)]
        public IFormFile image { get; set; }
        [Required(ErrorMessage = "You need to give us the job address")]
        [Display(Name = "Job address")]
        [DataType(DataType.MultilineText)]
        public string Job_location { get; set; }
        [Required(ErrorMessage = "You need to give us the categorie of this job")]
        [Display(Name = "Categorie ID")]
        public int Categorie_ID { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobCentrum_Website.Models
{
    public class ApplicationViewModel
    {
        [Required(ErrorMessage = "You have to give us your cv")]
        public IFormFile CV { get; set; }
        public string CV_Path { get; set; }
        [Required(ErrorMessage = "You have to give us why do you choose this job")]
        public string Motivation { get; set; }

        public int Apply_Id { get; set; }

        public JobViewModel Job { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Models
{
    public class ReviewViewModel
    {

        [Required]
        [StringLength(50)]
        public string Message { get; set; }

        [Required]
        [Range(0, 5)]
        public int Stars { get; set; }
    }
}

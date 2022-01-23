using Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Models
{
    public class ProductViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(0.0, 10000000000)]
        public double Price { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; }
    }
}

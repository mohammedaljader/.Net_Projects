using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataModels
{
    public class Review
    {
        public int ID { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public int Stars { get; set; }
        public string Message { get; set; }

    }
}

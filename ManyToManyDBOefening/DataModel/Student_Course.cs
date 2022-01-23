using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Student_Course
    {
        public Student StudentID  { get; set; }
        public Course CourseID { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;
using DataModel;

namespace ManyToManyDBOefening
{
    public partial class Form1 : Form
    {
        DBConnection conn = new DBConnection();
        private Student student = new Student();
        private Course course = new Course();
        User user = new User();
        JobSeeker jobSeeker = new JobSeeker();

     

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var studentCourses = conn.GetAllByStudent();

            //foreach (var data in studentCourses)
            //{
            //    MessageBox.Show(data.StudentID.FirstName +" + " + data.CourseID.Name);
            //}
            User user = new User()
            {
                Username = "Mohammed_THEKING2",
                Password = "Password.com",
                FullName = "Mohammed",
                Email= "THEking2@gmail.com",
                Telephone = "122211122",
                Address = "Eindhoven"
            };
            JobSeeker jobSeeker = new JobSeeker() 
            { 
                Experience = 6,
                Hobby = "Sporten en Fitnessen"
            };

            UserAccount userAccount = new UserAccount();
            userAccount.Register(user, jobSeeker);
            MessageBox.Show("Test seccessfully done!!!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            student.Student_ID = 2;
            course.CourseID = 3;
            Student_Course studentCourse = new Student_Course()
            {
                StudentID = student,
                CourseID = course,
                Datum = DateTime.Now
            };

            conn.ApplyToCourse(studentCourse);

        }
    }
}

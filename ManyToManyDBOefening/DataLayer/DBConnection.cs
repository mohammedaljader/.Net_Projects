using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using MySql.Data.MySqlClient;

namespace DataLayer
{
    public class DBConnection
    {
        private MySqlConnection connection = new MySqlConnection("server=127.0.0.1;uid=root;database=manytomanyoefening");

        public List<Student_Course> GetAllByStudent()
        {
            string quary = @"SELECT Student_name, c.Course_name  FROM student s  
            INNER JOIN student_course sc
            on s.Student_ID = sc.Student_ID
            INNER JOIN course c
            on c.Course_ID = sc.Course_ID
            ";
            List<Student_Course> geStudentCourses = new List<Student_Course>();
            try
            {
                using (connection)
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(quary, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Course sCourse = new Course()
                                {
                                    Name = reader.GetString(1)
                                };
                                Student student = new Student()
                                {
                                    FirstName = reader.GetString(0)
                                };
                                Student_Course review = new Student_Course()
                                {
                                    StudentID = student,
                                    CourseID = sCourse,
                                };
                                geStudentCourses.Add(review);
                            }
                            return geStudentCourses;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Fout");
            }
        }


        //
        public void ApplyToCourse(Student_Course studentCourse)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO student_course(Student_ID, Course_ID, Datum_Apply) VALUES (@Student_ID,@Course_ID, @Datum_Apply)", connection))
                    {
                        command.Parameters.AddWithValue("@Student_ID", studentCourse.StudentID.Student_ID);
                        command.Parameters.AddWithValue("@Course_ID", studentCourse.CourseID.CourseID);
                        command.Parameters.AddWithValue("@Datum_Apply", studentCourse.Datum);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}

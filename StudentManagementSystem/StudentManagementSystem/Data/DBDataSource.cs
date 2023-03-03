using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    class DBDataSource : IDataSource
    {

        private string ConnectionString = 
        "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";


        public void AddCourse(Course course)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Course VALUES (@CourseId, @CourseTitle, @Professor, @CourseDescription)";

                cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                cmd.Parameters.AddWithValue("@CourseTitle", course.CourseTitle);
                cmd.Parameters.AddWithValue("@Professor", course.Professor);
                cmd.Parameters.AddWithValue("@CourseDescription", course.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddCourseToStudent(Student student, Course course)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [StudentCourse]" +
                    "VALUES (@StudentId, @CourseId)";

                cmd.Parameters.AddWithValue("@StudentId", student.Id);
                cmd.Parameters.AddWithValue("@CourseId", course.CourseId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void AddStudent(Student student)
        {
            string connectionString = ConnectionString;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Student VALUES (@StudentId, @StudentName, @Age)";

                cmd.Parameters.AddWithValue("@StudentId", student.Id);
                cmd.Parameters.AddWithValue("@StudentName", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool DeleteCourse(Course course)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"DELETE FROM StudentCourse WHERE CourseId={course.CourseId}; DELETE FROM Course WHERE CourseId={course.CourseId}";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool DeleteStudent(Student student)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"DELETE FROM StudentCourse WHERE StudentId={student.Id}; DELETE FROM Student WHERE StudentId={student.Id}";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public void EditCourseInfo(Course courseToEdit, Course course)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE Course SET CourseTitle=@CourseTitle, Professor=@Professor, CourseDescription=@CourseDescription WHERE CourseId={courseToEdit.CourseId}";

                cmd.Parameters.AddWithValue("@CourseTitle", course.CourseTitle);
                cmd.Parameters.AddWithValue("@Professor", course.Professor);
                cmd.Parameters.AddWithValue("@CourseDescription", course.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditStudentInfo(Student studentToEdit, StudentInfoTransfer transfer)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE Student SET StudentName=@StudentName, Age=@Age WHERE StudentId={studentToEdit.Id}";

                cmd.Parameters.AddWithValue("@StudentName", transfer.Name);
                cmd.Parameters.AddWithValue("@Age", transfer.Age);


                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Course> GetCourses()
        {

            List<Course> courses = new List<Course>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Course";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var course = new Course();
                        course.CourseId = (int)dr["CourseId"];
                        course.CourseTitle = dr["CourseTitle"].ToString();
                        course.Description = dr["CourseDescription"].ToString();
                        course.Professor = dr["Professor"].ToString();

                        courses.Add(course);
                    }
                }
            }
            return courses;
        }

        public List<Student> GetStudents()
        {

            List<Student> students = new List<Student>();

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Student";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var student = new Student();
                        
                        student.Id = (int)dr["StudentId"];
                        student.Name = dr["StudentName"].ToString();
                        student.Age = (int)dr["Age"];
                        student.Courses = new List<Course>();

                        students.Add(student);
                    }
                }
            }

            List<Course> courses = GetCourses();

            List<StudentCourse> studentCourses = new List<StudentCourse>();

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"SELECT * FROM StudentCourse";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        StudentCourse sc = new StudentCourse()
                        {
                            StudentId = (int)dr["StudentId"],
                            CourseId = (int)dr["CourseId"]
                        };
                        studentCourses.Add(sc);
                    }
                }
            }

            foreach(var sc in studentCourses)
            {
                var student = students.SingleOrDefault(s => s.Id == sc.StudentId);
                var course = courses.SingleOrDefault(c => c.CourseId == sc.CourseId);

                student.Courses.Add(course);
            }
            return students;
        }

        public void RemoveCourseFromStudent(Student student, Course course)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"DELETE FROM StudentCourse WHERE StudentId={student.Id} AND CourseId={course.CourseId}";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

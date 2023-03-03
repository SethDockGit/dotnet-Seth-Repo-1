using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SqlTest.Models;

namespace SqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
           RemoveCourseFromStudent();
        }
        public static void RemoveCourseFromStudent()
        {
            string connectionString =
            "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            var studentId = 1;
            var courseId = 3;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"DELETE FROM StudentCourse WHERE StudentId={studentId} AND CourseId={courseId}";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void EditStudentInfo()
        {
            Student student = new Student()
            {
                Id = 2,
                Name = "Jeffer Deffer",
                Age = 31,
            };

            string connectionString = "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE Student SET StudentName=@StudentName, Age=@Age WHERE StudentId={student.Id}";

                cmd.Parameters.AddWithValue("@StudentName", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);


                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void EditCourseInfo()
        {
            Course courseToEdit = new Course()
            {
                CourseId = 4,
                CourseName = "Physics",
                Professor = "Dr. Bronners",
                Description = "Magnets...how do they work?"
            };
            Course newCourse = new Course()
            {
                CourseId = 4,
                CourseName = "Physics 1001",
                Professor = "Dr. Bronners",
                Description = "Magnets...how do they work?"
            };

            string connectionString = "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE Course SET CourseTitle=@CourseTitle, Professor=@Professor, CourseDescription=@CourseDescription WHERE CourseId={courseToEdit.CourseId}";

                cmd.Parameters.AddWithValue("@CourseTitle", newCourse.CourseName);
                cmd.Parameters.AddWithValue("@Professor", newCourse.Professor);
                cmd.Parameters.AddWithValue("@CourseDescription", newCourse.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static bool DeleteStudent()
        {
            string connectionString = "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM StudentCourse WHERE StudentId=4; DELETE FROM Student WHERE StudentId=4";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public static bool DeleteCourse()
        {
            string connectionString = "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM StudentCourse WHERE CourseId=1; DELETE FROM Course WHERE CourseId=1";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return true;
        }
        public static void AddStudent()
        {
            Student student = new Student()
            {
                Id = 4,
                Name = "Seth",
                Age = 30
            };

            string connectionString = "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

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

        public static void AddCourse()
        {

            Course course = new Course()
            {
                CourseId = 4,
                CourseName = "Physics",
                Professor = "Dr. Bronners",
                Description = "Magnets...how do they work?"
            };

            string connectionString = "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Course VALUES (@CourseId, @CourseTitle, @Professor, @CourseDescription)";

                cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                cmd.Parameters.AddWithValue("@CourseTitle", course.CourseName);
                cmd.Parameters.AddWithValue("@Professor", course.Professor);
                cmd.Parameters.AddWithValue("@CourseDescription", course.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static List<Course> GetCourses()
        {
            string ConnectionString =
            "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            List<Course> courses = new List<Course>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Course";

                conn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        var course = new Course();
                        course.CourseId = (int)dr["CourseId"];
                        course.CourseName = dr["CourseTitle"].ToString();
                        course.Description = dr["CourseDescription"].ToString();
                        course.Professor = dr["Professor"].ToString();

                        courses.Add(course);
                    }
                }
            }
            return courses;
        }
        public static List<Student> GetStudents()
        {
            string ConnectionString =
            "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            List<Student> students = new List<Student>();

            List<Course> courses = GetCourses();

            using(var conn = new SqlConnection())
            {
                conn.ConnectionString= ConnectionString;

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Student";

                conn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        var student = new Student();
                        student.Id = (int)dr["StudentId"];
                        student.Name = dr["StudentName"].ToString();
                        student.Age = (int)dr["Age"];

                        students.Add(student);
                    }
                }
            }

            foreach(var student in students)
            {
                student.Courses = new List<Course>();

                using(var conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnectionString;

                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT * FROM StudentCourse WHERE StudentId ={student.Id}";

                    conn.Open();
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            var courseId = (int)dr["CourseId"];

                            var course = courses.SingleOrDefault(c => c.CourseId == courseId);

                            student.Courses.Add(course);
                        }
                    }
                }
            }

            return students;
        }
        public static void AddCourseToStudent()
        {
            string connectionString =
            "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

            var studentId = 1;
            var courseId = 3;

            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [StudentCourse]" +
                    "VALUES (@StudentId, @CourseId)";

                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@CourseId", courseId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

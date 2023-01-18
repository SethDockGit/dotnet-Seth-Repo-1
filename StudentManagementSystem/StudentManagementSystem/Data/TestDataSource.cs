using System;
using System.Collections.Generic;
using System.Text;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class TestDataSource : IDataSource
    {
        public List<Student> Students { get; set; }

        public List<Course> Courses = new List<Course>()
        {
            new Course()
            {
                CourseId = 1,
                CourseName = "Politics Around the World",
                Professor = "Dr. Harry Potter",
                Description = "Countries around the world have governments. Check em out.",
            },
            new Course()
            {
                CourseId = 2,
                CourseName = "Study of people",
                Professor = "Dr. Norton Antivirus",
                Description = "There are lots of people. What are they doing?",
            },
            new Course()
            {
                CourseId = 3,
                CourseName = "Music Discourse",
                Professor = "Billy Gibbons",
                Description = "Is there a correlation between beards and quality of music?",
            },
        };

        public TestDataSource()
        {
            Students = GetStudents();
        }


        public List<Course> GetCourses()
        {
            return Courses;
        }

        public List<Student> GetStudents()
        {
            return new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    Name = "Anderson, Amantha",
                    Age = 42,
                    Courses = new List<Course>()
                },
                new Student()
                {
                    Id = 2,
                    Name = "Bobby, Danny",
                    Age = 87,
                    Courses = new List<Course>()
                },
                new Student()
                {
                    Id = 3,
                    Name = "Charizard, Jeff",
                    Age = 91,
                    Courses = new List<Course>()
                },
                new Student()
                {
                    Id = 4,
                    Name = "Dumpstendorf, Seth",
                    Age = 104,
                    Courses = new List<Course>()
                },
            };
        }

        public void RemoveStudent()
        {
            throw new NotImplementedException();
        }

        public void AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}

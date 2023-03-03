using System;
using System.Collections.Generic;
using System.Text;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class TestDataSource : IDataSource
    {
        public List<Student> Students = new List<Student>()
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

        public List<Course> Courses = new List<Course>()
        {
            new Course()
            {
                CourseId = 1,
                CourseTitle = "Politics Around the World",
                Professor = "Dr. Harry Potter",
                Description = "Countries around the world have governments. Check em out.",
            },
            new Course()
            {
                CourseId = 2,
                CourseTitle = "Study of people",
                Professor = "Dr. Norton Antivirus",
                Description = "There are lots of people. What are they doing?",
            },
            new Course()
            {
                CourseId = 3,
                CourseTitle = "Music Discourse",
                Professor = "Billy Gibbons",
                Description = "Is there a correlation between beards and quality of music?",
            },
        };

        public TestDataSource()
        {
            Students = GetStudents();
        }
        public List<Student> GetStudents()
        {
            return Students;
        }
        public List<Course> GetCourses()
        {
            return Courses;
        }
        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
        public bool DeleteStudent(Student student)
        {
            bool success = Students.Remove(student);

            return success;
        }
        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
        public void AddCourseToStudent(Student student, Course course)
        {
            student.Courses.Add(course);
        }
        public void RemoveCourseFromStudent(Student student, Course course)
        {
            student.Courses.Remove(course);
        }
        public void EditStudentInfo(Student studentToEdit, StudentInfoTransfer transfer)
        {
            studentToEdit.Age = transfer.Age;
            studentToEdit.Name = transfer.Name;
        }
        public void EditCourseInfo(Course courseToEdit, Course course)
        {
            courseToEdit.CourseTitle = course.CourseTitle;
            courseToEdit.Professor = course.Professor;
            courseToEdit.Description = course.Description;
        }
        public bool DeleteCourse(Course course)
        {
            bool success = Courses.Remove(course);

            return success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public interface IDataSource
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Student> GetStudents();
        public List<Course> GetCourses();
        public void AddStudent(Student student);
        public bool DeleteStudent(Student student);
        public void AddCourseToStudent(Student student, Course course);
        public void RemovecourseFromStudent(Student student, Course course);
        public void AddCourse(Course course);
        public void EditStudentInfo();
        public void EditCourseInfo();
        public bool DeleteCourse(Course course);
    }


}

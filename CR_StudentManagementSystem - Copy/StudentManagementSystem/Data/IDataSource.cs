using System;
using System.Collections.Generic;
using System.Text;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public interface IDataSource
    {
        public List<Student> GetStudents();
        public List<Course> GetCourses();
        public void AddStudent(Student student);
        public bool DeleteStudent(Student student);
        public void AddCourseToStudent(Student student, Course course);
        public void RemovecourseFromStudent(Student student, Course course);
        public void AddCourse(Course course);
        public void EditStudentInfo(Student studentToEdit, SInfoEditTransfer transfer);
        public void EditCourseInfo(Course courseToEdit, Course course);
        public bool DeleteCourse(Course course);
    }


}

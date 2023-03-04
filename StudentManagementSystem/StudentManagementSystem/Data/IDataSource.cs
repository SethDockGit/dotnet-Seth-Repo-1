﻿using System;
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
        public void DeleteStudent(Student student);
        public void AddCourseToStudent(Student student, Course course);
        public void RemoveCourseFromStudent(Student student, Course course);
        public void AddCourse(Course course);
        public void EditStudentInfo(Student studentToEdit, StudentInfoTransfer transfer);
        public void EditCourseInfo(Course courseToEdit, Course course);
        public void DeleteCourse(Course course);
    }


}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Logic
{
    public class Manager
    {
        public IDataSource IDataSource { get; set; }

        public Manager(IDataSource dataSource)
        {
            IDataSource = dataSource;
        }
        public WorkflowResponse GetStudents()
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            if(students == null)
            {
                response.Success = false;
                response.Message = "Unable to retrieve list of students.";
            }
            else if(students.Count == 0)
            {
                response.Success = true;
                response.Message = "There are currently 0 students enrolled.";
            }
            else
            {
                response.Success = true;
                response.Students = students;
            }
            return response;
        }

        public WorkflowResponse GetCourses()
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            if (courses == null)
            {
                response.Success = false;
                response.Message = "Unable to retrieve list of courses.";
            }
            else if (courses.Count == 0)
            {
                response.Success = true;
                response.Message = "There are currently no courses available.";
            }
            else
            {
                response.Success = true;
                response.Courses = courses;

            }
            return response;
        }

        public WorkflowResponse AddStudent(Student student)
        {
            WorkflowResponse response = new WorkflowResponse();

            if(IDataSource.Students.Count == 0)
            {
                student.Id = 1;
            }
            else
            {
                var highestID = IDataSource.Students.Max(s => s.Id); 

                student.Id = highestID + 1;
            }

            IDataSource.AddStudent(student);

            if(IDataSource.Students.Contains(student))
            {
                response.Success = true;
                response.Message = $"Student '{student.Name}' ID: {student.Id} Added successfully.";
            }
            else
            {
                throw new Exception("Error: Failed to add to student.");
            }

            return response;
        }
        public WorkflowResponse AddCourse(Course course)
        {
            WorkflowResponse response = new WorkflowResponse();

            IDataSource.AddCourse(course);

            if (IDataSource.Courses.Contains(course))
            {
                response.Success = true;
                response.Message = $"Course '{course.CourseName}' Added successfully.";
            }
            else
            {
                throw new Exception("Error: Failed to add to student.");
            }

            return response;
        }

        public WorkflowResponse DeleteStudent(int studentID)
        {
            WorkflowResponse response = new WorkflowResponse();

            var toDelete = IDataSource.Students.SingleOrDefault(s => s.Id == studentID);

            bool success = IDataSource.DeleteStudent(toDelete);

            if (success)
            {
                response.Success = true;
                response.Message = $"Student {toDelete.Name} successfully deleted.";
            }
            else
            {
                response.Success = false;
                response.Message = $"Error: student of ID {studentID} not found.";
            }

            return response;
        }

        public WorkflowResponse AddCourseToStudent(SCourseEditTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            var student = IDataSource.Students.SingleOrDefault(s => s.Id == transfer.StudentId);

            var course = IDataSource.Courses.SingleOrDefault(c => c.CourseId == transfer.CourseId);

            if (student == null)
            {
                response.Success = false;
                response.Message = "Error: student not found.";
            }
            else if (course == null)
            {
                response.Success = false;
                response.Message = "Error: course not found.";
            }
            else if (student.Courses.Any(c => c.CourseId == transfer.CourseId))
            {
                response.Success = false;
                response.Message = $"{student.Name} is already enrolled in that course";
            }
            else
            {
                response.Success = true;
                response.Message = $"Course {course.CourseName} successfully added to student {student.Name}.";
                IDataSource.AddCourseToStudent(student, course);
                
            }

            return response;
        }

        public WorkflowResponse RemoveCourseFromStudent(SCourseEditTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            var student = IDataSource.Students.SingleOrDefault(s => s.Id == transfer.StudentId);

            var course = student.Courses.SingleOrDefault(c => c.CourseId == transfer.CourseId);

            if (student == null)
            {
                response.Success = false;
                response.Message = "Error: student not found.";
            }
            else if (course == null)
            {
                response.Success = false;
                response.Message = "Error: course not found.";
            }
            else if (!student.Courses.Any(c => c.CourseId == transfer.CourseId))
            {
                response.Success = false;
                response.Message = $"{student.Name} is not enrolled in that course";
            }
            else
            {
                response.Success = true;
                response.Message = $"Course {course.CourseName} successfully removed from student {student.Name}.";
                IDataSource.RemovecourseFromStudent(student, course);
            }

            return response;
        }

        public WorkflowResponse EditStudentInfo(SInfoEditTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            var studentToEdit = IDataSource.Students.SingleOrDefault(s => s.Id == transfer.StudentId);

            studentToEdit.Age = transfer.Age;
            studentToEdit.Name = transfer.Name;

            IDataSource.EditStudentInfo();

            response.Success = true;
            response.Message = $"Information for {studentToEdit.Name} successfully updated";

            return response;

        }

        public WorkflowResponse EditCourseInfo(Course course)
        {
            WorkflowResponse response = new WorkflowResponse();

            var courseToEdit = IDataSource.Courses.SingleOrDefault(c => c.CourseId == course.CourseId);

            courseToEdit.CourseName = course.CourseName;
            courseToEdit.Professor = course.Professor;
            courseToEdit.Description = course.Description;

            IDataSource.EditCourseInfo();

            response.Success = true;
            response.Message = $"Information for course {courseToEdit.CourseName} successfully updated";

            return response;
        }
        public WorkflowResponse DeleteCourse(int ID)
        {
            WorkflowResponse response = new WorkflowResponse();

            var toDelete = IDataSource.Courses.SingleOrDefault(c => c.CourseId == ID);

            bool success = IDataSource.DeleteCourse(toDelete);

            if (success)
            {
                response.Success = true;
                response.Message = $"Course {toDelete.CourseName} successfully deleted.";
            }
            else
            {
                response.Success = false;
                response.Message = $"Error: course of ID {ID} not found."; //can this even happen?
            }

            return response;
        }
    }
}

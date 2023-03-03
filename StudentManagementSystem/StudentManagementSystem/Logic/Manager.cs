using System;
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

            if(students.Count == 0)
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

            if (courses.Count == 0)
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

            List<Student> students = IDataSource.GetStudents();

            if (students.Count == 0)
            {
                student.Id = 1;
            }
            else
            {
                var highestID = students.Max(s => s.Id); 

                student.Id = highestID + 1;
            }

            IDataSource.AddStudent(student);

            students = IDataSource.GetStudents();

            if(students.Contains(student))
            {
                response.Success = true;
                response.Message = $"Student '{student.Name}' ID: {student.Id} Added successfully.";
            }
            else
            {
                throw new Exception("Error: Failed to add student.");
            }

            return response;
        }
        public WorkflowResponse AddCourse(Course course)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            if (courses.Count == 0)
            {
                course.CourseId = 1;
            }
            else
            {
                var highestID = courses.Max(c => c.CourseId);

                course.CourseId = highestID + 1;
            }

            IDataSource.AddCourse(course);

            courses = IDataSource.GetCourses();

            if (courses.Contains(course))  
            {
                response.Success = true;
                response.Message = $"Course '{course.CourseName}' Added successfully.";
            }
            else
            {
                throw new Exception("Error: Failed to add course.");
            }

            return response;
        }
        public WorkflowResponse DeleteStudent(int studentID)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            var toDelete = students.SingleOrDefault(s => s.Id == studentID);

            bool success = IDataSource.DeleteStudent(toDelete);

            if (success)
            {
                response.Success = true;
                response.Message = $"Student {toDelete.Name} successfully deleted.";
            }
            else
            {
                throw new Exception("Error: Student not found.");
            }

            return response;
        }
        public WorkflowResponse AddCourseToStudent(SCourseEditTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            List<Course> courses = IDataSource.GetCourses();

            var student = students.SingleOrDefault(s => s.Id == transfer.StudentId);

            var course = courses.SingleOrDefault(c => c.CourseId == transfer.CourseId);

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

            List<Student> students = IDataSource.GetStudents();

            List<Course> courses = IDataSource.GetCourses();

            var student = students.SingleOrDefault(s => s.Id == transfer.StudentId);

            var course = courses.SingleOrDefault(c => c.CourseId == transfer.CourseId);

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
                IDataSource.RemoveCourseFromStudent(student, course);
            }

            return response;
        }
        public WorkflowResponse EditStudentInfo(SInfoEditTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            var studentToEdit = students.SingleOrDefault(s => s.Id == transfer.StudentId);

            IDataSource.EditStudentInfo(studentToEdit, transfer);

            response.Success = true;
            response.Message = $"Information for {studentToEdit.Name} successfully updated";

            return response;

        }
        public WorkflowResponse EditCourseInfo(Course course)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            var courseToEdit = courses.SingleOrDefault(c => c.CourseId == course.CourseId);

            IDataSource.EditCourseInfo(courseToEdit, course);

            response.Success = true;
            response.Message = $"Information for course {courseToEdit.CourseName} successfully updated";

            return response;
        }
        public WorkflowResponse DeleteCourse(int ID)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            var toDelete = courses.SingleOrDefault(c => c.CourseId == ID);

            bool success = IDataSource.DeleteCourse(toDelete);

            if (success)
            {
                response.Success = true;
                response.Message = $"Course {toDelete.CourseName} successfully deleted.";
            }
            else
            {
                response.Success = false;
                response.Message = $"Error: course of ID {ID} not found."; 
            }

            return response;
        }
        public Course GetCourseByID(int id)
        {
            List<Course> courses = IDataSource.GetCourses();

            Course toGet = courses.SingleOrDefault(c => c.CourseId == id);

            return toGet;
        }
    }
}

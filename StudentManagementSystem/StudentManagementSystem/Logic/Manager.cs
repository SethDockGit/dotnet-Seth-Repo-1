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

            Exception ex = new Exception();

            try
            {
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
            }
            catch(Exception e)
            {
                response.Success=false;
                response.Message = e.Message + e.StackTrace;

                ex = e;
            }

            return response;

        }
        public WorkflowResponse GetCourses()
        {
            WorkflowResponse response = new WorkflowResponse();

            Exception ex = new Exception();

            try
            {
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
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;

                ex = e;
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

            Exception ex = new Exception();

            try
            {
                IDataSource.AddStudent(student);
                response.Success = true;
                response.Message = $"Student '{student.Name}' ID: {student.Id} Added successfully.";
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;

                ex = e;
            }

            return response;
        }
        public WorkflowResponse AddCourse(Course course)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            Exception ex = new Exception();

            if (courses.Count == 0)
            {
                course.CourseId = 1;
            }
            else
            {
                var highestID = courses.Max(c => c.CourseId);

                course.CourseId = highestID + 1;
            }

            try
            {
                IDataSource.AddCourse(course);
                response.Success = true;
                response.Message = $"Course '{course.CourseTitle}' Added successfully.";
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;

                ex = e;
            }
 
            return response;
        }
        public WorkflowResponse DeleteStudent(int studentID)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            var toDelete = students.SingleOrDefault(s => s.Id == studentID);

            Exception ex = new Exception();
            try
            {
                bool success = IDataSource.DeleteStudent(toDelete);

                if(success)
                {
                    response.Success = true;
                    response.Message = $"Student {toDelete.Name} successfully deleted.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Error: Unable to delete student.";
                }
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;

                ex = e;
            }

            return response;
        }
        public WorkflowResponse AddCourseToStudent(StudentCourse sc)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            List<Course> courses = IDataSource.GetCourses();

            var student = students.SingleOrDefault(s => s.Id == sc.StudentId);

            var course = courses.SingleOrDefault(c => c.CourseId == sc.CourseId);

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
            else if (student.Courses.Any(c => c.CourseId == sc.CourseId))
            {
                response.Success = false;
                response.Message = $"{student.Name} is already enrolled in that course";
            }
            else
            {
                Exception ex = new Exception();
                try
                {
                    IDataSource.AddCourseToStudent(student, course);
                    response.Success = true;
                    response.Message = $"Course {course.CourseTitle} successfully added to student {student.Name}.";           
                }
                catch(Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message + e.StackTrace;

                    ex = e;
                }
            }

            return response;
        }
        public WorkflowResponse RemoveCourseFromStudent(StudentCourse sc)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            List<Course> courses = IDataSource.GetCourses();

            var student = students.SingleOrDefault(s => s.Id == sc.StudentId);

            var course = courses.SingleOrDefault(c => c.CourseId == sc.CourseId);

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
            else if (!student.Courses.Any(c => c.CourseId == sc.CourseId))
            {
                response.Success = false;
                response.Message = $"{student.Name} is not enrolled in that course";
            }
            else
            {
                Exception ex = new Exception();
                try
                {
                    IDataSource.RemoveCourseFromStudent(student, course);
                    response.Success = true;
                    response.Message = $"Course {course.CourseTitle} successfully removed from student {student.Name}.";
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message + e.StackTrace;

                    ex = e;
                }
            }

            return response;
        }
        public WorkflowResponse EditStudentInfo(StudentInfoTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            var studentToEdit = students.SingleOrDefault(s => s.Id == transfer.StudentId);

            Exception ex = new Exception();

            try
            {
                IDataSource.EditStudentInfo(studentToEdit, transfer);

                response.Success = true;
                response.Message = $"Information for {studentToEdit.Name} successfully updated";
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;

                ex = e;
            }

            return response;

        }
        public WorkflowResponse EditCourseInfo(Course course)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            var courseToEdit = courses.SingleOrDefault(c => c.CourseId == course.CourseId);

            Exception ex = new Exception();
            try
            {
                IDataSource.EditCourseInfo(courseToEdit, course);

                response.Success = true;
                response.Message = $"Information for course {courseToEdit.CourseTitle} successfully updated";
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;

                ex = e;
            }

            return response;
        }
        public WorkflowResponse DeleteCourse(int ID)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            var toDelete = courses.SingleOrDefault(c => c.CourseId == ID);

            Exception ex = new Exception();

            try
            {
                bool success = IDataSource.DeleteCourse(toDelete);

                if (success)
                {
                    response.Success = true;
                    response.Message = $"Course {toDelete.CourseTitle} successfully deleted.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Error: Unable to delete course."; 
                }
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;

                ex = e;
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

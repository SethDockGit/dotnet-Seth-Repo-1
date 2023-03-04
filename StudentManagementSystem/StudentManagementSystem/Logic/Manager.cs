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
            }

            return response;

        }
        public WorkflowResponse GetCourses()
        {
            WorkflowResponse response = new WorkflowResponse();

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
            }
 
            return response;
        }
        public WorkflowResponse DeleteStudent(int studentID)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            var toDelete = students.SingleOrDefault(s => s.Id == studentID);

            try
            {
                IDataSource.DeleteStudent(toDelete);

                response.Success = true;
                response.Message = $"Student {toDelete.Name} successfully deleted.";

            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
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
                try
                {
                    IDataSource.AddCourseToStudent(student, course);
                    response.Success = true;
                    response.Message = $"Course {course.CourseTitle} successfully added to student {student.Name}.";           
                }
                catch(Exception e)
                {
                    response.Success = false;
                    response.Message = "Error: Failed to add course to student." + e.StackTrace;
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
                try
                {
                    IDataSource.RemoveCourseFromStudent(student, course);
                    response.Success = true;
                    response.Message = $"Course {course.CourseTitle} successfully removed from student {student.Name}.";
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = "Error: Failed to remove course from student." + e.StackTrace;
                }
            }

            return response;
        }
        public WorkflowResponse EditStudentInfo(StudentInfoTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Student> students = IDataSource.GetStudents();

            var studentToEdit = students.SingleOrDefault(s => s.Id == transfer.StudentId);

            try
            {
                IDataSource.EditStudentInfo(studentToEdit, transfer);

                response.Success = true;
                response.Message = $"Information for {studentToEdit.Name} successfully updated";
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = "Error: Failed to update student info." + e.StackTrace;
            }

            return response;

        }
        public WorkflowResponse EditCourseInfo(Course course)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            var courseToEdit = courses.SingleOrDefault(c => c.CourseId == course.CourseId);

            try
            {
                IDataSource.EditCourseInfo(courseToEdit, course);

                response.Success = true;
                response.Message = $"Information for course {courseToEdit.CourseTitle} successfully updated";
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = "Error: Failed to update course info." + e.StackTrace;
            }

            return response;
        }
        public WorkflowResponse DeleteCourse(int ID)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Course> courses = IDataSource.GetCourses();

            var toDelete = courses.SingleOrDefault(c => c.CourseId == ID);

            try
            {
                IDataSource.DeleteCourse(toDelete);

                response.Success = true;
                response.Message = $"Course {toDelete.CourseTitle} successfully deleted.";
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
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

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
                response.Success = false;
                response.Message = "There are currently 0 students enrolled.";
            }
            else
            {
                response.Success = true;
                response.Students = students;
                //message?
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
                response.Success = false;
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

            var highestID = IDataSource.Students.Max(s => s.Id);

            student.Id = highestID + 1;

            IDataSource.AddStudent(student);

            if(IDataSource.Students.Contains(student))
            {
                response.Success = true;
                response.Message = $"Student '{student.Name}' ID: {student.Id} Added successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = $"Error: Unable to add student.";
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

        public WorkflowResponse AddCourses(int studentID, int[] courseIDs)
        {
            throw new NotImplementedException();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using StudentManagementSystem.Logic;
using StudentManagementSystem.Models;

namespace StudentManagerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        public Manager Manager { get; set; }
        public StudentController()
        {
            var managerFactory = new ManagerFactory();
            Manager = managerFactory.GetManager();
        }

        [HttpGet]
        [Route("students")]
        public WorkflowResponse GetStudents()
        {
            WorkflowResponse response = Manager.GetStudents();

            return response;
        }
        [HttpGet]
        [Route("courses")]
        public WorkflowResponse GetCourses()
        {
            WorkflowResponse response = Manager.GetCourses();

            return response;
        }
        [HttpPost]
        [Route("addstudent")]
        public WorkflowResponse AddStudent([FromBody] Student student)
        {
            WorkflowResponse response = Manager.AddStudent(student);

            return response;
        }
        [HttpPost]
        [Route("editstudent")]
        public WorkflowResponse EditStudent([FromBody] StudentInfoTransfer transfer)
        {
            WorkflowResponse response = Manager.EditStudentInfo(transfer);

            return response;
        }
        [HttpPost]
        [Route("addcourse")]
        public WorkflowResponse AddCourse([FromBody] Course course)
        {
            WorkflowResponse response = Manager.AddCourse(course);

            return response;
        }
        [HttpDelete]
        [Route("s{id}")]
        public WorkflowResponse DeleteStudent(int id)
        {
            WorkflowResponse response = Manager.DeleteStudent(id);

            return response;
        }
        [HttpDelete]
        [Route("c{id}")]
        public WorkflowResponse DeleteCourse(int id)
        {
            WorkflowResponse response = Manager.DeleteCourse(id);

            return response;
        }
        [HttpPost]
        [Route("addstudentcourse")]
        public WorkflowResponse AddCourseToStudent([FromBody] StudentCourse sc)
        {

            WorkflowResponse response = Manager.AddCourseToStudent(sc);

            return response;

        }
        [HttpPost]
        [Route("dropstudentcourse")]
        public WorkflowResponse RemoveCourseFromStudent([FromBody] StudentCourse sc)
        {
            WorkflowResponse response = Manager.RemoveCourseFromStudent(sc);

            return response;
        }
        [HttpPost]
        [Route("editcourse")]
        public WorkflowResponse EditCourse([FromBody] Course course)
        {
            WorkflowResponse response = Manager.EditCourseInfo(course);

            return response;
        }
    }
}

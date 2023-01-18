using System;
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
        public WorkflowResponse AddStudent([FromBody]Student student)
        {
            WorkflowResponse response = Manager.AddStudent(student);

            return response;
        }
        [HttpDelete]
        [Route("{id}")]
        public WorkflowResponse DeleteStudent(int id)
        {
            WorkflowResponse response = Manager.DeleteStudent(id);

            return response;
        }
        [HttpPost]
        [Route("addstudentcourse")]
        public WorkflowResponse AddCourseToStudent([FromBody]SCourseEditTransfer transfer)
        {

            WorkflowResponse response = Manager.AddCourseToStudent(transfer);

            return response;

        }
        [HttpPost]
        [Route("removestudentcourse")]
        public WorkflowResponse RemoveCourseFromStudent([FromBody]SCourseEditTransfer transfer)
        {
            WorkflowResponse response = Manager.RemoveCourseFromStudent(transfer);

            return response;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroToApI.Data;
using IntroToApI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        public DataSource DataSource = new DataSource();
        

        [HttpGet]
        public List<Student> GetStudents()
        {

            return DataSource.Students;
        }
        [HttpGet]
        [Route("{id}")]
        public Student GetStudentById(int id)
        {
            var student = DataSource.Students.SingleOrDefault(s => s.Id == id);

            return student;
        }

        [HttpPost]
        [Route("add")]
        public Response AddStudent([FromBody]Student student)
        {

            var response = DataSource.AddStudent(student.Name, student.Age);

            return response;

        }

        [HttpDelete]
        [Route("{id}")]
        public string DeleteStudentById(int id)
        {
            var student = DataSource.Students.SingleOrDefault(s => s.Id == id);
            var message = "";


            if(student == null)
            {
                message = $"Error: Student of id {id} not found.";
            }
            else
            {
                DataSource.Students.Remove(student);
                message = $"Student {student.Id}:{student.Name} removed.";
            }

            return message;
        }
    }

}

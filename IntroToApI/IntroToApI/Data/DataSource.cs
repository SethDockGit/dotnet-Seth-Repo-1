using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntroToApI.Models;

namespace IntroToApI.Data
{
    public class DataSource
    {
        public List<Student> Students { get; set; } = new List<Student>()
        {
            new Student()
            {
                Id = 0,
                Name = "Sally",
                Age = 32
            },
            new Student()
            {
                Id = 1,
                Name = "Doug",
                Age = 16
            },
            new Student()
            {
                Id = 2,
                Name = "Brandon",
                Age = 35
            },
            new Student()
            {
                Id = 3,
                Name = "Jon",
                Age = 35
            },
        };

        public Response AddStudent(string name, int age)
        {

            var response = new Response();

            if(name == "")
            {
                response.Success = false;
                response.Message = "Error: A name must be provided";
            }
            else
            {

                var newStudent = new Student();

                var highestId = Students.Max(s => s.Id);

                newStudent.Id = highestId + 1;
                newStudent.Name = name;
                newStudent.Age = age;

                Students.Add(newStudent);

                response.Success = true;
                response.Message = $"Successfully added {name} to students";

            }

            return response;
        }

    }
}

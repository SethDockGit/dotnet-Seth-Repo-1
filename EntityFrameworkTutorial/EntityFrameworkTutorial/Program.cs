using System;
using EntityFrameworkTutorial.Models;
using System.Linq;

namespace EntityFrameworkTutorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        private static void AddStudent()
        {

            using (var context = new UniContext())
            {

                var student = new Student
                {
                    LastName = "Khan",
                    FirstMidName = "Ali",
                    EnrollmentDate = DateTime.Parse("2005-09-01")
                };

                context.Students.Add(student);
                context.SaveChanges();
            }
        }
        private static void ChangeStudent()
        {

            using (var context = new UniContext())
            {

                var student = (from d in context.Students
                               where d.FirstMidName == "Ali"
                               select d).Single();
                student.LastName = "Aslam";
                context.SaveChanges();
            }
        }
        private static void DeleteStudent()
        {

            using (var context = new UniContext())
            {
                var bay = (from d in context.Students where d.FirstMidName == "Ali" select d).Single();
                context.Students.Remove(bay);
                context.SaveChanges();
            }
        }
    }
}

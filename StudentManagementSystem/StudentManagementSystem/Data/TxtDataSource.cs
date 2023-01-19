using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class TxtDataSource : IDataSource
    {

        public string StudentSaveFile { get; set; }
        public string CourseSaveFile { get; set; }
        public List<Course> Courses { get; set; }
        public List<Student> Students { get; set; }

        public TxtDataSource()
        {
            StudentSaveFile = @"C:\Data\StudentManagement\students.txt";
            CourseSaveFile = @"C:\Data\StudentManagement\courses.txt";

            Students = new List<Student>();
            Courses = new List<Course>();

            if (File.Exists(StudentSaveFile))
            {
                PopulateStudents();
            }
            else
            {
                File.Create(StudentSaveFile);
            }
            if (File.Exists(CourseSaveFile))
            {
                PopulateCourses();
            }
            else
            {
                File.Create(CourseSaveFile);
            }
        }

        private void PopulateStudents()
        {
            using (StreamReader sr = File.OpenText(StudentSaveFile))
            {
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitline = line.Split('^');

                    Student student = new Student()
                    {
                        Id = int.Parse(splitline[0]),
                        Name = splitline[1],
                        Age = int.Parse(splitline[2]),
                        Courses = new List<Course>()
                    };

                    string[] courses = splitline[3].Split('*');

                    for (int i = 1; i < courses.Length; i++)
                    {
                        string[] courseProperties = courses[i].Split('&'); ///try line instead of courses[i] if fail

                        Course course = new Course()
                        {
                            CourseId = int.Parse(courseProperties[0]),
                            CourseName = courseProperties[1],
                            Professor = courseProperties[2],
                            Description = courseProperties[3],
                        };

                        student.Courses.Add(course);
                    }

                    Students.Add(student);
                }
            }
        }

        public void ReWriteStudentsFile()
        {
            File.Delete(StudentSaveFile);

            File.Create(StudentSaveFile).Close();

            foreach(var student in Students)
            {
                if(Students.Count > 0)
                {
                    using (StreamWriter sw = File.AppendText(StudentSaveFile))
                    {
                        if (student != Students.First())
                        {
                            sw.Write("\n");
                        }
                        sw.Write($"{student.Id}^{student.Name}^{student.Age}^");

                        foreach (var course in student.Courses)
                        {
                            sw.Write($"*{course.CourseId}&{course.CourseName}&{course.Professor}&{course.Description}");
                        }
                    }
                }
            }

            PopulateStudents();
        }


        public List<Student> GetStudents()
        {
            return Students;
        }

        public List<Course> GetCourses()
        {
            return Courses;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);

            ReWriteStudentsFile();
        }

        public bool DeleteStudent(Student student)
        {
            bool success = Students.Remove(student);

            ReWriteStudentsFile();

            return success;
        }
        private void PopulateCourses()
        {
            using (StreamReader sr = File.OpenText(CourseSaveFile))
            {
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitline = line.Split(',');

                    Course course = new Course()
                    {
                        CourseId = int.Parse(splitline[0]),
                        CourseName = splitline[1],
                        Professor = splitline[2],
                        Description = splitline[3],
                    };

                    Courses.Add(course);
                }
            }
        }
        private void ReWriteCourseFile()
        {
            throw new NotImplementedException();
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);

            ReWriteCourseFile();
        }

        public void AddCourseToStudent(Student student, Course course)
        {
            student.Courses.Add(course);

            ReWriteStudentsFile();
        }

        public void RemovecourseFromStudent(Student student, Course course)
        {
            student.Courses.Remove(course);

            ReWriteStudentsFile();
        }
    }
}

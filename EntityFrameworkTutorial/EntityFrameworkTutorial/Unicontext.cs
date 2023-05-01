using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using EntityFrameworkTutorial.Models;

namespace EntityFrameworkTutorial
{
    public class UniContext : DbContext
    {
        public UniContext() : base("UniContext") { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

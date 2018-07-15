using SchoolMVCEF.Models;
using System.Data.Entity;

namespace SchoolMVCEF.Models
{
    public class SchoolSystemDb : DbContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
} 
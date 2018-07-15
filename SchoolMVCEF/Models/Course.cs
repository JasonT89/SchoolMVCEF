using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVCEF.Models
{
    public class Course
    {
        [Required]
        public int CourseId { get; set; }

        public string CourseName { get; set; }
        public List<Teacher> CourseTeachers { get; set; }
        public List<Student> CourseStudents { get; set; }
        public List<Assignment> CourseAssignments { get; set; }
        public string CourseSubject { get; internal set; }
    }
}
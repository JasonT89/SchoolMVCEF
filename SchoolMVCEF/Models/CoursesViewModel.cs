using SchoolMVCEF.Models;
using System.Collections.Generic;

namespace SchoolMVCEF.Models
{
    public class CoursesViewModel
    {
        public int CourseId { get; set; }

        public string CourseSubject { get; set; }

        public string CourseName { get; set; }

        public List<Teacher> CourseTeachers { get; set; }
        public List<Assignment> CourseAssignments { get; set; }
        public List<Student> CourseStudents { get; set; }

        public CoursesViewModel(Course course)
        {
            CourseId = course.CourseId;
            CourseSubject = course.CourseSubject;
            CourseName = course.CourseName;
            CourseTeachers = new List<Teacher>();
            CourseAssignments = new List<Assignment>();
            CourseStudents = new List<Student>();


            foreach (var item in course.CourseTeachers)
            {
                CourseTeachers.Add(item);
            }

            foreach (var item in course.CourseAssignments)
            {
                CourseAssignments.Add(item);
            }

            foreach (var item in course.CourseStudents)
            {
                CourseStudents.Add(item);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVCEF.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string AssignmentBody { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string AssignmentSubject { get; internal set; }
    }
}
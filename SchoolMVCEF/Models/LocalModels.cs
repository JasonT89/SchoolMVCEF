using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVCEF.Models
{
    public static class LocalModels
    {
        public static List<CoursesViewModel> MakeListCourses()
        {
            var _db = new SchoolSystemDb();

            List<CoursesViewModel> ViewModels = new List<CoursesViewModel>();

            var model = _db.Courses.Include("CourseStudents")
                        .Include("CourseTeachers")
                        .Include("CourseAssignments").ToList();
            foreach (var item in model)
            {

                ViewModels.Add(new CoursesViewModel(item));
            }

            return ViewModels;
        }

        public static List<TeachersViewModel> MakeListTeachers()
        {
            var _db = new SchoolSystemDb();

            List<TeachersViewModel> ViewModels = new List<TeachersViewModel>();

            var model = _db.Teachers.Include("Courses").ToList();

            foreach (var item in model)
            {

                ViewModels.Add(new TeachersViewModel(item));
            }

            return ViewModels;
        }

        public static List<StudentsViewModel> MakeListStudents()
        {
            var _db = new SchoolSystemDb();

            List<StudentsViewModel> ViewModels = new List<StudentsViewModel>();

            var model = _db.Students.Include("Courses").ToList();
            var model2 = _db.Assignments.ToList();

            foreach (var item in model)
            {

                ViewModels.Add(new StudentsViewModel(item, model2));
            }

            return ViewModels;
        }
    }
}
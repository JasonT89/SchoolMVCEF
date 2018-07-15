using SchoolMVCEF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SchoolSystem.Controllers
{
    public class HomeController : Controller
    {
        SchoolSystemDb _db = new SchoolSystemDb();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            var model = _db.Courses.ToList();

            return View(model);
        }    

        public ActionResult EditAll()
        {

            return View();
        }

        [HttpPost]
        public ActionResult EditAll(string edit)
        {

            if (edit == "Courses")
            {
                List<CoursesViewModel> ViewModels = new List<CoursesViewModel>();
                ViewModels = LocalModels.MakeListCourses();


                return View("EditCourses", ViewModels);
            }
            else if (edit == "Teachers")
            {
                List<TeachersViewModel> ViewModels = new List<TeachersViewModel>();
                ViewModels = LocalModels.MakeListTeachers();


                return View("ShowEditTeachers", ViewModels);
            } else if(edit == "Students")
            {
                List<StudentsViewModel> ViewModels = new List<StudentsViewModel>();
                ViewModels = LocalModels.MakeListStudents();


                return View("ShowEditStudents", ViewModels);
            }

            return View();
        }

        public ActionResult EditTeacher(int TeacherId)
        {
            var model = _db.Teachers.Include("Courses")
                                    .ToList()
                                    .Find(x => x.TeacherId == TeacherId);

            TeachersViewModel teacher = new TeachersViewModel(model);
            return View(teacher);
        }

        [HttpPost]
        public ActionResult EditTeacher(string Firstname, string Lastname, string AddCourse, int id, params int[] Courses)
        {
            var teacher = _db.Teachers.Include("Courses")
                                      .ToList()
                                      .Find(x => x.TeacherId == id);
            var coursesDb = _db.Courses.ToList();

            if (teacher != null)
            {
                teacher.FirstName = Firstname;
                teacher.LastName = Lastname;

                if (Courses != null)
                {
                    foreach (var item in Courses)
                    {
                        var course = teacher.Courses.Find(x => x.CourseId == item);
                        teacher.Courses.Remove(course);
                    }
                }

                foreach (var item in coursesDb)
                {
                    if (item.CourseName == AddCourse)
                    {
                        teacher.Courses.Add(item);
                        break;
                    }
                }

                _db.SaveChanges();
            }
            return RedirectToAction("EditAll");
        }

        public ActionResult EditStudent(int StudentId)
        {
            var model = _db.Students.Include("Courses")
                                    .ToList()
                                    .Find(x => x.StudentId == StudentId);

            var model2 = _db.Assignments.ToList();

            StudentsViewModel student = new StudentsViewModel(model, model2);

            return View(student);
        }

        [HttpPost]
        public ActionResult EditStudent(string Firstname, string Lastname, string AddCourse, int id, params int[] Courses)
        {
            var student = _db.Students.Include("Courses")
                                      .ToList()
                                      .Find(x => x.StudentId == id);
            
            var coursesDb = _db.Courses.ToList();

            if (student != null)
            {
                student.FirstName = Firstname;
                student.LastName = Lastname;

                if (Courses != null)
                {
                    foreach (var item in Courses)
                    {
                        var course = student.Courses.Find(x => x.CourseId == item);
                        student.Courses.Remove(course);
                    }
                }

                foreach (var item in coursesDb)
                {
                    if (item.CourseName == AddCourse)
                    {
                        student.Courses.Add(item);
                        break;
                    }
                }

                _db.SaveChanges();
            }
            return RedirectToAction("EditAll");
        }

        public ActionResult Details(int? id)
        {
            Course course = _db.Courses.Find(id);

            return View(course);
        }

        public ActionResult Students(int id)
        {
            var model = _db.Courses.Include("CourseStudents")
                                    .Include("CourseTeachers")
                                    .Include("CourseAssignments")
                                    .ToList().Find(x => x.CourseId == id);
            CoursesViewModel coursesViewModel = new CoursesViewModel(model);
                                    
            if (coursesViewModel == null)
            {
                return RedirectToAction("Index");
            }

            return PartialView("_Details", coursesViewModel);
        }

        public ActionResult Add()
        {
            List<CoursesViewModel> ViewModels = new List<CoursesViewModel>();

            var model = _db.Courses.Include("CourseStudents")
                                    .Include("CourseTeachers")
                                    .Include("CourseAssignments")
                                    .ToList();
            foreach (var item in model)
            {

                ViewModels.Add(new CoursesViewModel(item));
            }

            return View(ViewModels);
        }

        [HttpPost]
        public ActionResult Add(string FirstnameIn, string LastnameIn, params int[] Assignments)
        {
            Student student = new Student
            {
                FirstName = FirstnameIn,
                LastName = LastnameIn
            };

            if (Assignments != null)
            {
                foreach (var item in Assignments)
                {
                    var assignment = _db.Courses.Find(item);

                    student.Courses.Add(assignment);
                }
            }

            _db.Students.Add(student);

            _db.SaveChanges();
            return RedirectToAction("About");
        }

        public ActionResult AddCourse(string SubjectIn, string NameIn, params int[] Assignments)
        {
            Course course = new Course
            {
                CourseSubject = SubjectIn,
                CourseName = NameIn
            };

            if (Assignments != null)
            {
                foreach (var item in Assignments)
                {
                    var assignment = _db.Assignments.Find(item);
                    course.CourseAssignments.Add(assignment);
                } 
            }

            _db.Courses.Add(course);
            _db.SaveChanges();
           
            return RedirectToAction("AddAll");
        }

        public ActionResult AddAssignment(string SubjectIn, string BodyIn, int Courses)
        {
            Assignment assignment = new Assignment
            {
                AssignmentSubject = SubjectIn,
                AssignmentBody = BodyIn,
                Course = _db.Courses.Find(Courses)

            };

            _db.Assignments.Add(assignment);
            _db.SaveChanges();

            return RedirectToAction("AddAll");
        }       

        public ActionResult AddTeacher(string FirstnameIn, string LastnameIn, params int[] Courses)
        {
            Teacher teacher = new Teacher
            {
                FirstName = FirstnameIn,
                LastName = LastnameIn
            };

            if (Courses != null)
            {
                foreach (var item in Courses)
                {
                    var course = _db.Courses.Find(item);
                    teacher.Courses.Add(course);
                } 
            }
            _db.Teachers.Add(teacher);
            _db.SaveChanges();

            return RedirectToAction("AddAll");
        }

        public ActionResult AddAll()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAll(string add)
        {
            if (add == "Course")
            {
                var model = _db.Assignments.ToList();
                return View("AddCourse", model);
            }
            else if (add == "Assignment")
            {
                var model = _db.Courses.ToList();
                return View("AddAssignment", model);
            }
            else if (add == "Teacher")
            {
                var model = _db.Courses.ToList();
                return View("AddTeacher", model);
            }
            else if (add == "Student")
            {
                return RedirectToAction("Add");
            }
            return View();
        }

        public ActionResult Delete(int? StudentId, int? CourseId, int? TeacherId)
        {
            if (StudentId != null && CourseId != null)
            {
                var model = _db.Students.Include("Courses").ToList();

                var model2 = model.Find(x => x.StudentId == StudentId)
                    .Courses
                    .Find(x => x.CourseId == CourseId);

                _db.Students.Find(StudentId).Courses.Remove(model2);
                _db.SaveChanges();
            }
            else if (TeacherId != null && CourseId != null)
            {
                var model = _db.Teachers.Include("Courses").ToList();

                var model2 = model.Find(x => x.TeacherId == TeacherId)
                    .Courses
                    .Find(x => x.CourseId == CourseId);

                _db.Teachers.Find(TeacherId).Courses.Remove(model2);
                _db.SaveChanges();
            }
            else if (CourseId != null)
            {
                var model = _db.Courses.Find(CourseId);
                _db.Courses.Remove(model);
                _db.SaveChanges();

            }
            else if (StudentId != null)
            {
                var model = _db.Students.Find(StudentId);
                _db.Students.Remove(model);
                _db.SaveChanges();
            }
            else if (TeacherId != null)
            {
                var model = _db.Teachers.Find(TeacherId);
                _db.Teachers.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("EditAll");
        }
    }
}
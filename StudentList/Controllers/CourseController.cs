using Microsoft.AspNetCore.Mvc;
using StudentList.DataCntxt;
using StudentList.DTOs;
using StudentList.Models;

namespace StudentList.Controllers
{
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly DataCntext _context;
        public CourseController(DataCntext db)
        {
            _context = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Course> studentlist = _context.Courses.ToList();
            return View(studentlist);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost("Create")] // day ko phai REST
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Course>> Create([FromForm] CourseDTO createCourse) // cai nay dung Form chu ko phai Body
        {
            if (ModelState.IsValid)
            {
                var createNewCourse = new Course()
                {
                    CourseName = createCourse.CourseName
                };
                _context.Courses.Add(createNewCourse);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(new CourseResponseDTO
            {
                CourseId = createCourse.CourseId,
                CourseName = createCourse.CourseName
            });
        }

        [HttpGet("Edit/{courseid}")]
        public IActionResult Edit([FromRoute] int courseid)
        {
            if (courseid == null)
            {
                return NotFound();
            }
            var GetCourseId = _context.Courses.Find(courseid);
            return View(GetCourseId);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] CourseDTO course)
        {
            var courseToUpdate = _context.Courses.Find(id);
            if (courseToUpdate == null)
            {
                // xu ly hien thi validate error

                return View(course);
            }

            if (ModelState.IsValid) // xu ly tinh dung dan cua du lieu body truyen vao
            {// đoạn này framework đang tự xử lý validate gái trị truyền vào, cái Course đang null, em tự sửa tiếp nhé
                courseToUpdate.CourseName = course.CourseName;
                _context.Courses.Update(courseToUpdate);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // xu ly hien thi update error
            return View(course);

        }

        [HttpGet("Delete/{courseid}")]
        public IActionResult Delete([FromRoute] int courseid)
        {
            if (courseid == null)
            {
                return NotFound();
            }
            var GetcourseId = _context.Courses.Find(courseid);
            return View(GetcourseId);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, [FromForm] CourseDTO course)
        {
            var courseToDelete = _context.Courses.Find(id);
            if (courseToDelete == null)
            {
                // xu ly hien thi validate error

                return View(course);
            }

            if (ModelState.IsValid) // xu ly tinh dung dan cua du lieu body truyen vao
            {// đoạn này framework đang tự xử lý validate gái trị truyền vào, cái Course đang null, em tự sửa tiếp nhé
                courseToDelete.CourseName = course.CourseName;
                _context.Courses.Remove(courseToDelete);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // xu ly hien thi update error
            return View(course);

        }
    }
}

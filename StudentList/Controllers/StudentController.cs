using Microsoft.AspNetCore.Mvc;
using StudentList.DataCntxt;
using StudentList.DTOs;
using StudentList.Models;

namespace StudentList.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly DataCntext _context;
        public StudentController(DataCntext db) {
            _context = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Student> studentlist = _context.Students.ToList();
            return View(studentlist);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost("Create")] // day ko phai REST
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Student>> Create([FromForm] StudentDTO createStudent) // cai nay dung Form chu ko phai Body
        {
            if (ModelState.IsValid)
            {
                var createNewStudent = new Student()
                {

                    StudentName = createStudent.StudentName,
                    ClassName = createStudent.ClassName

                };
                _context.Students.Add(createNewStudent);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(new StudentResponseDTO
            {
                StudentName = createStudent.StudentName,
                ClassName = createStudent.ClassName
            });
        }

        [HttpGet("Edit/{studentid}")]
        public  IActionResult Edit([FromRoute] int studentid)
        {
            if (studentid == null) 
            {
                return NotFound();
            }
            var GetStudentId = _context.Students.Find(studentid);
            return View(GetStudentId);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] StudentDTO student)
        {
            var studentToUpdate = _context.Students.Find(id);
            if (studentToUpdate == null)
            {
                // xu ly hien thi validate error

                return View(student);
            }

            if (ModelState.IsValid) // xu ly tinh dung dan cua du lieu body truyen vao
            {// đoạn này framework đang tự xử lý validate gái trị truyền vào, cái Course đang null, em tự sửa tiếp nhé
                studentToUpdate.ClassName = student.ClassName;
                studentToUpdate.StudentName = student.StudentName;
                studentToUpdate.CourseName = student.CourseName;
                _context.Students.Update(studentToUpdate);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // xu ly hien thi update error
            return View(student);

        }

        [HttpGet("Delete/{studentid}")]
        public IActionResult Delete([FromRoute] int studentid)
        {
            if (studentid == null)
            {
                return NotFound();
            }
            var GetStudentId = _context.Students.Find(studentid);
            return View(GetStudentId);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, [FromForm] StudentDTO student)
        {
            var studentToDelete = _context.Students.Find(id);
            if (studentToDelete == null)
            {
                // xu ly hien thi validate error

                return View(student);
            }

            if (ModelState.IsValid) // xu ly tinh dung dan cua du lieu body truyen vao
            {// đoạn này framework đang tự xử lý validate gái trị truyền vào, cái Course đang null, em tự sửa tiếp nhé
                studentToDelete.ClassName = student.ClassName;
                studentToDelete.StudentName = student.StudentName;
                studentToDelete.CourseName = student.CourseName;
                _context.Students.Remove(studentToDelete);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // xu ly hien thi update error
            return View(student);

        }
    }
}

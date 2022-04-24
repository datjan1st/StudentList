using System.ComponentModel.DataAnnotations;

namespace StudentList.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public List<Course> Course { get; set; } // ko dùng model để giao tiếp với view, dùng DTO
    }
}

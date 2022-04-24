using System.ComponentModel.DataAnnotations;

namespace StudentList.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Student> Student { get; set; } 
    }
}

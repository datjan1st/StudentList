namespace StudentList.DTOs
{
    public class CourseResponseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<StudentDTO> studentDTOs { get; set; }
    }
}

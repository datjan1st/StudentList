namespace StudentList.DTOs
{
    public class StudentResponseDTO
    {
        public string StudentName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public List<CourseDTO> courseDTOs { get; set; }

    }
}

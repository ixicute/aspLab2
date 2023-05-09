namespace SkolprojektLab2.ViewModels
{
    public class StudentTeacherSearchViewModel
    {
        public int FK_RelationshipTable { get; set; }
        public int CourseId { get; set; }
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public string CourseName { get; set; }
    }
}

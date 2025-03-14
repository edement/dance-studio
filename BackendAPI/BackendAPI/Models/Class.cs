namespace BackendAPI.Models
{
    public class Class
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid CoachId { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}

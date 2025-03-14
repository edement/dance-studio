namespace BackendAPI.Models
{
    public class Enrollment
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; }
    }
}

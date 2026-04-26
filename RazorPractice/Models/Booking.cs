namespace RazorPractice.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string EmployeeName { get; set; }
        public string Comment { get; set; }
    }
}

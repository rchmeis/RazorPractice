namespace RazorPractice.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Equipment { get; set; } // e.g., "Projector, Whiteboard"
        public bool IsOccupied { get; set; }

        public Room() { }

        
    }
}

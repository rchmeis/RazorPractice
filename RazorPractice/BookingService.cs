using RazorPractice.Models;
using RazorPractice.Repositories;

namespace RazorPractice
{
    public class BookingService
    {
        private readonly IBookingRepository _repo; 

        public BookingService(IBookingRepository repo) => _repo = repo;

        public bool IsRoomAvailable(int roomId, DateTime start, DateTime end)
        {
            var allBookings = _repo.GetAllBookings();

            // Filter bookings for this specific room
            var roomBookings = allBookings.Where(b => b.RoomId == roomId);

            // Check for overlaps
            bool hasOverlap = roomBookings.Any(existing =>
                start < existing.EndTime && end > existing.StartTime
            );

            return !hasOverlap; // If there is no overlap, it is available
        }

        public void CreateBooking(Booking booking)
        {
            if (IsRoomAvailable(booking.RoomId, booking.StartTime, booking.EndTime))
            {
                _repo.AddBooking(booking);
            }
            else
            {
                throw new Exception("Room is already booked for this time period.");
            }
        }
        public List<Booking> GetAllBookings()
        {
            // The service asks the repository for the data and passes it up to the UI
            return _repo.GetAllBookings();
        }
    }
}

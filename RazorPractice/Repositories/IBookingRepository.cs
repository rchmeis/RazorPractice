using RazorPractice.Models;

namespace RazorPractice.Repositories
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();
        void SaveBookings(List<Booking> bookings);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking updatedBooking);
        void DeleteBooking(int id);
    }
}

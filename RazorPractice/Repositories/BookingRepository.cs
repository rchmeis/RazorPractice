using System.Text.Json;
using RazorPractice.Models;

namespace RazorPractice.Repositories
{
    public class BookingRepository :IBookingRepository
    {
        private readonly string _filePath = @"Data/bookings.json";

        public BookingRepository()
        {
            
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
        }

        public List<Booking> GetAllBookings()
        {
            if (!File.Exists(_filePath))
                return new List<Booking>();

            var json = File.ReadAllText(_filePath);

            // Handle empty files or invalid JSON
            return JsonSerializer.Deserialize<List<Booking>>(json) ?? new List<Booking>();
        }

        public void SaveBookings(List<Booking> bookings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(bookings, options);
            File.WriteAllText(_filePath, json);
        }

        public void AddBooking(Booking booking)
        {
            var bookings = GetAllBookings();

            // Auto-increment ID: find the current max ID and add 1
            booking.Id = bookings.Any() ? bookings.Max(b => b.Id) + 1 : 1;

            bookings.Add(booking);
            SaveBookings(bookings);
        }

        public void UpdateBooking(Booking updatedBooking)
        {
            var bookings = GetAllBookings();
            var index = bookings.FindIndex(b => b.Id == updatedBooking.Id);

            if (index != -1)
            {
                bookings[index] = updatedBooking;
                SaveBookings(bookings);
            }
        }

        public void DeleteBooking(int id)
        {
            var bookings = GetAllBookings();
            var bookingToRemove = bookings.FirstOrDefault(b => b.Id == id);

            if (bookingToRemove != null)
            {
                bookings.Remove(bookingToRemove);
                SaveBookings(bookings);
            }
        }

    }
}

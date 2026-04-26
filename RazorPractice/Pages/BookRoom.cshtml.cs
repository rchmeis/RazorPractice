using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPractice.Models;


namespace RazorPractice.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;

        public BookRoomModel(BookingService bookingService, RoomService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }

        [BindProperty]
        public Booking NewBooking { get; set; }

        public Room Room { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(int id)
        {
            // Find the room being booked to display its name on the UI
            Room = _roomService.GetRooms(null, null).FirstOrDefault(r => r.Id == id);

            // Initialize booking with the RoomId and default times
            NewBooking = new Booking
            {
                RoomId = id,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2)
            };
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) // If the data is invalid (e.g. name missing), show the user why
            {
                // IMPORTANT: We must re-fetch the room info so the UI doesn't break
                Room = _roomService.GetRooms(null, null).FirstOrDefault(r => r.Id == NewBooking.RoomId);
                return Page();
            }

            // 1. Business Logic: Check if available
            bool isAvailable = _bookingService.IsRoomAvailable(
                NewBooking.RoomId, NewBooking.StartTime, NewBooking.EndTime);

            if (!isAvailable)
            {
                ModelState.AddModelError(string.Empty, "The room is already booked for this time!");
                Room = _roomService.GetRooms(null, null).FirstOrDefault(r => r.Id == NewBooking.RoomId);
                return Page();
            }

            // 2. Logic: Create the booking
            _bookingService.CreateBooking(NewBooking);
            return RedirectToPage("Rooms");
        }
    }
}

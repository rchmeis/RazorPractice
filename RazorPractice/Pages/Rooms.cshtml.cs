using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPractice.Models;

namespace RazorPractice.Pages
{
    public class RoomsModel : PageModel
    {
        private readonly RoomService _roomService;
        private readonly BookingService _bookingService;
        public RoomsModel(RoomService roomservice, BookingService bookingservice)
        {
            _roomService = roomservice;
            _bookingService = bookingservice;
        }

        public List<Room> Rooms { get; set; }
        public Dictionary<int, bool> RoomStatus { get; set; } = new Dictionary<int, bool>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public void OnGet()
        {
            Rooms = _roomService.GetRooms(SearchTerm, null);
            var allBookings = _bookingService.GetAllBookings(); 

            foreach (var room in Rooms)
            {
                bool occupied = _roomService.IsRoomOccupiedNow(room.Id, allBookings);
                RoomStatus.Add(room.Id, occupied);
            }
        }
        public IActionResult OnPostDelete(int id)
        {
            _roomService.RemoveRoom(id);
            return RedirectToPage();
        }
    }
}

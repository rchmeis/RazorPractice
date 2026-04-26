using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPractice.Models;

namespace RazorPractice.Pages
{
    public class AddRoomModel : PageModel
    {
        private readonly RoomService _roomService;

        public AddRoomModel(RoomService roomService)
        {
            _roomService = roomService;
        }
        [BindProperty]
        public Room NewRoom { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _roomService.CreateRoom(NewRoom);
            return RedirectToPage("Rooms"); // Goes back to the list after saving
        }
    }
}

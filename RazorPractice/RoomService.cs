using RazorPractice.Models;
using RazorPractice.Repositories;

namespace RazorPractice
{
    public class RoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public List<Room> GetRooms(string? search, int? minCapacity)
        {
            var rooms = _repository.GetAllRooms();

            if (!string.IsNullOrEmpty(search))
            {
                rooms = rooms.Where(r => r.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         r.Equipment.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (minCapacity.HasValue)
            {
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value).ToList();
            }

            return rooms;
        }
        public bool IsRoomOccupiedNow(int roomId, List<Booking> allBookings)
        {
            DateTime now = DateTime.Now;

            // A room is occupied if 'now' falls between any booking's Start and End time
            return allBookings.Any(b =>
                b.RoomId == roomId &&
                now >= b.StartTime &&
                now <= b.EndTime);
        }

        public void CreateRoom(Room room) => _repository.AddRoom(room);
        public void RemoveRoom(int id) => _repository.DeleteRoom(id);
        public void UpdateRoom(Room room) => _repository.UpdateRoom(room);
    }
}


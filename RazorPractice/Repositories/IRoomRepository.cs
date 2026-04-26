using RazorPractice.Models;

namespace RazorPractice.Repositories
{
    public interface IRoomRepository
    {
        List<Room> GetAllRooms();
        void SaveRooms(List<Room> rooms);
        void AddRoom(Room room);
        void DeleteRoom(int id);
        void UpdateRoom(Room updatedRoom);
    }
}

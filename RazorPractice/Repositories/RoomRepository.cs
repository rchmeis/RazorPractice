using RazorPractice.Models;
using System.Text.Json;

namespace RazorPractice.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _filePath = @"Data/rooms.json";

        public List<Room> GetAllRooms()
        {
            if (!File.Exists(_filePath)) return new List<Room>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Room>>(json) ?? new List<Room>();
        }

        public void SaveRooms(List<Room> rooms)
        {
            var json = JsonSerializer.Serialize(rooms, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void AddRoom(Room room)
        {
            var rooms = GetAllRooms();
            room.Id = rooms.Any() ? rooms.Max(r => r.Id) + 1 : 1;
            rooms.Add(room);
            SaveRooms(rooms);
        }

        public void DeleteRoom(int id)
        {
            var rooms = GetAllRooms();
            rooms.RemoveAll(r => r.Id == id);
            SaveRooms(rooms);
        }

        public void UpdateRoom(Room updatedRoom)
        {
            var rooms = GetAllRooms();
            var index = rooms.FindIndex(r => r.Id == updatedRoom.Id);
            if (index != -1)
            {
                rooms[index] = updatedRoom;
                SaveRooms(rooms);
            }
        }

    }
}

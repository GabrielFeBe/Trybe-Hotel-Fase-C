using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 7. Refatore o endpoint GET /room
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var rooms = _context.Rooms.Include(room => room.Hotel).ThenInclude(hotel => hotel.City).ToList();

            var roomDto = from room in rooms
                          select new RoomDto
                          {
                              roomId = room.RoomId,
                              name = room.Name,
                              capacity = room.Capacity,
                              image = room.Image,
                              hotel = new HotelDto
                              {
                                  hotelId = room.Hotel.HotelId,
                                  name = room.Hotel.Name,
                                  address = room.Hotel.Address,
                                  cityId = room.Hotel.CityId,
                                  cityName = room.Hotel.City.Name,
                                  state = room.Hotel.City.State
                              }
                          };


            return roomDto;

        }

        // 8. Refatore o endpoint POST /room
        public RoomDto AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            var roomCreated = GetRooms(room.HotelId).FirstOrDefault(r => r.roomId == room.RoomId, new RoomDto
            {

            });

            return roomCreated;
        }

        public void DeleteRoom(int RoomId)
        {
            throw new NotImplementedException();
        }
    }
}
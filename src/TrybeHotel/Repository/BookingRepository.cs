using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 9. Refatore o endpoint POST /booking
        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var room = GetRoomById(booking.RoomId);


            if (room.capacity < booking.GuestQuant)
            {
                throw new Exception("Guest quantity over room capacity");
            }

            var user = _context.Users.FirstOrDefault(user => user.Email == email);
            var response = _context.Bookings.Add(new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                RoomId = booking.RoomId,
                UserId = user.UserId
            });
            _context.SaveChanges();
            return new BookingResponse
            {
                bookingId = response.Entity.BookingId,
                checkIn = response.Entity.CheckIn,
                checkOut = response.Entity.CheckOut,
                guestQuant = response.Entity.GuestQuant,
                room = room,
            };
        }

        // 10. Refatore o endpoint GET /booking
        public BookingResponse GetBooking(int bookingId, string email)
        {
            var bookingReturn = _context.Bookings.Include(booking => booking.Room)
            .ThenInclude(room => room.Hotel).ThenInclude(hotel => hotel.City).FirstOrDefault(booking => booking.BookingId == bookingId);

            var user = _context.Users.Where(user => user.Email == email).FirstOrDefault();
            if (user.UserId != bookingReturn.UserId)
            {
                throw new Exception("User not found");
            }


            return new BookingResponse
            {
                bookingId = bookingReturn.BookingId,
                checkIn = bookingReturn.CheckIn,
                checkOut = bookingReturn.CheckOut,
                guestQuant = bookingReturn.GuestQuant,
                room = GetRoomById(bookingReturn.RoomId)


            };
        }

        public RoomDto GetRoomById(int RoomId)
        {
            var room = _context.Rooms.Include(room => room.Hotel).ThenInclude(hotel => hotel.City).FirstOrDefault(room => room.RoomId == RoomId);

            var roomDto = new RoomDto
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

    }

}
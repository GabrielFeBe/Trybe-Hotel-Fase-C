using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        //  5. Refatore o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            var hotels = _context.Hotels.Include(hotel => hotel.City).ToList();
            var hotelsDto = from hotel in hotels
                            select new HotelDto
                            {
                                address = hotel.Address,
                                hotelId = hotel.HotelId,
                                name = hotel.Name,
                                cityId = hotel.CityId,
                                cityName = hotel.City.Name,
                                state = hotel.City.State
                            };
            return hotelsDto;
        }

        // 6. Refatore o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            throw new NotImplementedException();
        }
    }
}
using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            throw new NotImplementedException();
        }

        // 2. Refatore o endpoint POST /city
        public CityDto AddCity(City city)
        {

            var createdCity = _context.Cities.Add(city);
            _context.SaveChanges();
            return new CityDto
            {
                cityId = createdCity.Entity.CityId,
                name = createdCity.Entity.Name,
                state = createdCity.Entity.State
            };
        }
        // 3. Desenvolva o endpoint PUT /city
        public CityDto UpdateCity(City city)
        {
            var existingCity = _context.Cities.Find(city.CityId);
            existingCity.State = city.State;
            existingCity.Name = city.Name;
            existingCity.Hotels = city.Hotels;
            _context.SaveChanges();
            return new CityDto
            {
                cityId = existingCity.CityId,
                name = existingCity.Name,
                state = existingCity.State
            };


        }

    }
}
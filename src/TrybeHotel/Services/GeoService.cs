using System.Net.Http;
using System.Security.Policy;
using TrybeHotel.Dto;
using TrybeHotel.Repository;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://nominatim.openstreetmap.org/";
        public GeoService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("User-Agent", "aspnet-user-agent");
            _client.BaseAddress = new Uri(_baseUrl);
        }

        // 11. Desenvolva o endpoint GET /geo/status
        public async Task<object> GetGeoStatus()
        {
            var response = await _client.GetAsync("status.php?format=json");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Não foi possível obter o status");
            }

            var result = await response.Content.ReadFromJsonAsync<object>();
            return result;
        }

        // 12. Desenvolva o endpoint GET /geo/address
        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            var response = await _client.GetAsync($"search?street={geoDto.Address}&city={geoDto.City}&country=Brazil&state={geoDto.State}&format=json&limit=1");
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }
            var result = await response.Content.ReadFromJsonAsync<List<GeoDtoResponse>>();
            var geoDtoResponse = from geo in result
                                 select new GeoDtoResponse
                                 {
                                     lat = geo.lat,
                                     lon = geo.lon

                                 };
            return geoDtoResponse.FirstOrDefault();
        }

        // 12. Desenvolva o endpoint GET /geo/address
        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var geoLocation = await GetGeoLocation(geoDto);


            var hotels = repository.GetHotels();

            var hotelAndLocationTasks = hotels.Select(async h => new
            {
                hotelId = h.hotelId,
                name = h.name,
                address = h.address,
                cityName = h.cityName,
                state = h.state,
                local = await GetGeoLocation(new GeoDto { Address = h.address, City = h.cityName, State = h.state }),
            });
            var hotelAndLocationsResult = await Task.WhenAll(hotelAndLocationTasks);


            var geoDtoRes = from hotel in hotelAndLocationsResult
                            select new GeoDtoHotelResponse
                            {
                                HotelId = hotel.hotelId,
                                Name = hotel.name,
                                Address = hotel.address,
                                CityName = hotel.cityName,
                                State = hotel.state,
                                Distance = CalculateDistance(geoLocation.lat, geoLocation.lon, hotel.local.lat, hotel.local.lon)
                            };

            return geoDtoRes.ToList();
        }



        public int CalculateDistance(string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny)
        {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.', ','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.', ','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.', ','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.', ','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return int.Parse(Math.Round(distance, 0).ToString());
        }

        public double radiano(double degree)
        {
            return degree * Math.PI / 180;
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using TrybeHotel.Services;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("login")]

    public class LoginController : Controller
    {

        private readonly IUserRepository _repository;
        public LoginController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login)
        {

            try
            {
                var response = _repository.Login(login);
                var generator = new TokenGenerator();
                return Ok(new { token = generator.Generate(response) });
            }
            catch (Exception e)
            {
                var conflictResponse = new
                {
                    message = e.Message
                };
                return Unauthorized(conflictResponse);
            }
        }
    }
}
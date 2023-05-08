using Azure.Core;
using EcomWeb.Dtos.Auth;
using EcomWeb.Models;
using EcomWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EcomWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private MyDbContext _context;
        private TokenService _tokenService;
        public AuthController(MyDbContext context, TokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                var user = _context.Users.Include(x => x.Role).SingleOrDefault(x => x.Email == loginDto.Email);

                if (user == null)
                {
                    throw new Exception("This user doesn't exist");
                }

                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    throw new Exception("Invalid password");
                }

                var token = _tokenService.GenerateToken(user);

                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Login success",
                    Data = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

    }
}

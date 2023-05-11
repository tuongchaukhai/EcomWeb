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

        [HttpPost("CustomerLogin")]
        public async Task<IActionResult> CustomerLogin(LoginDto loginDto)
        {
            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(x => x.Email == loginDto.Email);

                if (customer == null)
                {
                    throw new Exception("This email doesn't exist");
                }

                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, customer.Password))
                {
                    throw new Exception("Invalid password");
                }

                var token = _tokenService.CustomerGenerateToken(customer);

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

        [HttpPost("CustomerRegister")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var emailExists = await _context.Customers.SingleOrDefaultAsync(b => b.Email == registerDto.Email);
                if (emailExists != null)
                    throw new Exception("This email already exists.");

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

                var customer = new Customer
                {
                    Email = registerDto.Email,
                    FullName = registerDto.FullName,
                    Password = passwordHash,
                    Active = true,
                    Phone = registerDto.Phone
                };

                await _context.AddAsync(customer);
                await _context.SaveChangesAsync();

                var token = _tokenService.CustomerGenerateToken(customer);

                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Register success",
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

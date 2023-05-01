using AutoMapper;
using EcomWeb.Dtos.User;
using EcomWeb.Models;
using EcomWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

 
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Get all users successfully.",
                Data = _mapper.Map<IEnumerable<UserResultDto>>(users)
            });
        }

        [HttpPost]
        public IActionResult Create(UserAddDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var userAdd = _userService.Create(user);
            if(userAdd == null) {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "The email already exists."
                });
            }

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "User created successfully.",
                Data = _mapper.Map<UserResultDto>(userAdd)
            });

        }
    }
}

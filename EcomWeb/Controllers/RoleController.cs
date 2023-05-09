using AutoMapper;
using EcomWeb.Dtos.Role;
using EcomWeb.Models;
using EcomWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private IRoleService _roleService;
        private IMapper _mapper;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Get all roles successfully.",
                Data = _mapper.Map<IEnumerable<RoleResultDto>>(roles)
            });
        }

        [HttpPost]
        public ActionResult Create(RoleAddDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);

            var roleAdd = _roleService.Create(role);
            if(roleAdd == null)
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "This role name is already exists."
                });
            }

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Role created successfully.",
                Data = _mapper.Map<RoleResultDto>(roleAdd)
            });
        }
    }
}

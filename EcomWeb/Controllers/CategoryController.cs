using AutoMapper;
using EcomWeb.Dtos.Category;
using EcomWeb.Models;
using EcomWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Get all categories successfully.",
                Data = _mapper.Map<IEnumerable<CategoryResultDto>>(categories)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddDto categoryDto)
        {
            try
            {
                var category = await _categoryService.Create(_mapper.Map<Category>(categoryDto));

                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Category created successfully.",
                    Data = _mapper.Map<CategoryResultDto>(category)
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryDto)
        {
            try
            {
                var category = await _categoryService.Update(_mapper.Map<Category>(categoryDto));

                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Category updated sccuessfully.",
                    Data = _mapper.Map<CategoryResultDto>(category)
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _categoryService.GetById(id);

                await _categoryService.Delete(category);
                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Category removed sccuessfully.",
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

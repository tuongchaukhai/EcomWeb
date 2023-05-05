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
            var category = await _categoryService.Create(_mapper.Map<Category>(categoryDto));
            if (category == null)
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "This category name already exists."
                });

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Category created successfully.",
                Data = _mapper.Map<CategoryResultDto>(category)
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryDto)
        {
            var category = await _categoryService.Update(_mapper.Map<Category>(categoryDto));
            if (category == null)
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "Category doesn't exists.",
                });

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Category updated sccuessfully.",
                Data = _mapper.Map<CategoryResultDto>(category)
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetById(id);
            if(category == null)
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "Category doesn't exists.",
                });

            await _categoryService.Delete(category);
            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Category removed sccuessfully.",
            });
        }
    }
}

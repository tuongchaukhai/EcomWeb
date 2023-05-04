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
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAll();

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Get all categories successfully.",
                Data = _mapper.Map<IEnumerable<CategoryResultDto>>(categories)
            });
        }

        [HttpPost]
        public IActionResult Create(CategoryAddDto categoryDto)
        {
            var category = _categoryService.Create(_mapper.Map<Category>(categoryDto));
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
        public IActionResult Update(CategoryUpdateDto categoryDto)
        {
            var category = _categoryService.Update(_mapper.Map<Category>(categoryDto));
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
        public IActionResult Delete(CategoryResultDto categoryDto)
        {
            if(!_categoryService.Delete(_mapper.Map<Category>(categoryDto)))
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "Category doesn't exists.",
                });

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Category removed sccuessfully.",
            });
        }
    }
}

using AutoMapper;
using EcomWeb.Dtos.Product;
using EcomWeb.Models;
using EcomWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using static System.Reflection.Metadata.BlobBuilder;

namespace EcomWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var products = await _productService.GetAll();

            if (!products.Any()) return NotFound();

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Get all products successfully.",
                Data = _mapper.Map<IEnumerable<ProductResultDto>>(products)
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductAddDto productDto)
        {
            var product = await _productService.Create(_mapper.Map<Product>(productDto));
            if (product != null)
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "This title is already exists."
                });

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Product created successfully.",
                Data = _mapper.Map<ProductResultDto>(product)
            });
        }

        [HttpPut]
        public async Task<ActionResult> Update(ProductUpdateDto productDto)
        {
            var productEdit = await _productService.GetById(productDto.ProductId);
            _mapper.Map(productDto, productEdit);
            var product = await _productService.Update(productEdit);

            //var product = await _productService.Update(_mapper.Map<Product>(productDto));

            if (product == null)
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "This product doesn't exists."
                });

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Product updated successfully.",
                Data = _mapper.Map<ProductResultDto>(product)
            });
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "This product doesn't exists."
                });

            await _productService.Delete(product);
            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Product removed successfully.",
            });
        }
    }
}

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
        public ActionResult GetAll()
        {
            var products = _productService.GetAll();

            if (!products.Any()) return NotFound();

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Get all products successfully.",
                Data = _mapper.Map<IEnumerable<ProductResultDto>>(products)
            });
        }

        [HttpPost]
        public ActionResult Create(ProductAddDto productDto)
        {
            var product = _productService.Create(_mapper.Map<Product>(productDto));
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
        public ActionResult Update(ProductUpdateDto productDto)
        {
            var productEdit = _productService.GetById(productDto.ProductId);

            _mapper.Map(productDto, productEdit);
            var product = _productService.Update(productEdit);

            if (product == null)
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "This product doesn't exists."
                });

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Product created successfully.",
                Data = _mapper.Map<ProductResultDto>(product)
            });
        }

        [HttpDelete]
        public ActionResult Delete(ProductResultDto productDto)
        {
            if(!_productService.Delete(_mapper.Map<Product>(productDto)))
                 return BadRequest(new ApiResponse
                 {
                     StatusCode = 400,
                     Message = "This product doesn't exists."
                 });

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Product removed successfully.",
            });
        }
    }
}

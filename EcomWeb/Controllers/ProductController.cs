using AutoMapper;
using CsvHelper;
using EcomWeb.Dtos.Product;
using EcomWeb.Models;
using EcomWeb.Services;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
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
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var products = await _productService.GetAll(page, pageSize);

            //if (!products.Products.Any()) return NotFound();

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Get all products successfully.",
                Data = new { products = _mapper.Map<IEnumerable<ProductResultDto>>(products.Products), totalRecords = products.TotalRecords }
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductAddDto productDto)
        {
            try
            {
                var product = await _productService.Create(_mapper.Map<Product>(productDto));

                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Product created successfully.",
                    Data = _mapper.Map<ProductResultDto>(product)
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

        [HttpPut]
        public async Task<ActionResult> Update(ProductUpdateDto productDto)
        {
            try
            {
                var productEdit = await _productService.GetById(productDto.ProductId);
                _mapper.Map(productDto, productEdit);
                var product = await _productService.Update(productEdit);

                //var product = await _productService.Update(_mapper.Map<Product>(productDto));

                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Product updated successfully.",
                    Data = _mapper.Map<ProductResultDto>(product)
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
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetById(id);

                await _productService.Delete(product);
                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Product removed successfully.",
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


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest(new ApiResponse { StatusCode = 400, Message = "No file uploaded" });

            var fileExtension = Path.GetExtension(file.FileName); https://localhost:7209/api/Product/upload
            if (fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                using (var stream = file.OpenReadStream())
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            var productDto = new ProductAddDto
                            {
                                ProductName = reader.GetValue(0).ToString(),
                                CategoryId = reader.GetInt32(1),
                                UnitInStock = reader.GetInt32(2),
                                Price = reader.GetInt32(3),
                                Discount = reader.GetInt32(4) != null ? reader.GetInt32(4) : null,
                                Description = reader.GetValue(5).ToString(),
                                Thumb = reader.GetString(6),
                                Video = reader.GetString(7),
                                Tags = reader.GetString(8),
                                BestSeller = reader.GetBoolean(9),
                                HomeFlag = reader.GetBoolean(10),
                                Active = reader.GetBoolean(11),
                                ShortDesc = reader.GetString(12),
                                Alias = reader.GetString(13),
                                MetaDesc = reader.GetString(14),
                                MetaKey = reader.GetString(15)
                            };
                            await _productService.Create(_mapper.Map<Product>(productDto));
                            //if fail...
                        }
                    } while (reader.NextResult());
                }
            }
            else if (fileExtension == ".csv")
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    while (csv.Read())
                    {
                        var productDto = new ProductAddDto
                        {
                            ProductName = csv.GetField(0),
                            CategoryId = csv.GetField<int>(1),
                            UnitInStock = csv.GetField<int>(2),
                            Price = csv.GetField<int>(3),
                            Discount = csv.GetField<int>(4) != null ? csv.GetField<int>(4) : null,
                            Description = csv.GetField(5),
                            Thumb = csv.GetField(6),
                            Video = csv.GetField(7),
                            Tags = csv.GetField(8),
                            BestSeller = csv.GetField<bool>(9),
                            HomeFlag = csv.GetField<bool>(10),
                            Active = csv.GetField<bool>(11),
                            ShortDesc = csv.GetField(12),
                            Alias = csv.GetField(13),
                            MetaDesc = csv.GetField(14),
                            MetaKey = csv.GetField(15)
                        };
                        await _productService.Create(_mapper.Map<Product>(productDto));
                    }
                }
            }
            else
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "Unsupported file format."
                }
                    );
            }
            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "File uploaded and processed successfully."
            });
        }

    }
}

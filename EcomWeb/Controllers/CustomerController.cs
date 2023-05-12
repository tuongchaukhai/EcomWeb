using AutoMapper;
using CsvHelper;
using EcomWeb.Dtos.Customer;
using EcomWeb.Dtos.Product;
using EcomWeb.Models;
using EcomWeb.Services;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace EcomWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var customers = await _customerService.GetAll(page, pageSize);

            if (!customers.Customers.Any()) return NotFound();

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Get all customers successfully.",
                Data = new { customers = _mapper.Map<IEnumerable<CustomerResultDto>>(customers.Customers), totalRecords = customers.TotalRecords }
            });
        }

        [HttpGet("Export")]
        public async Task<IActionResult> ExportData()
        {
            try
            {
                var customers = await _customerService.ExportData();
                var customersResult = _mapper.Map<IEnumerable<CustomerResultDto>>(customers);

                using (var memoryStream = new MemoryStream())
                {
                    var writer = new StreamWriter(memoryStream, Encoding.UTF8);
                    var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

                    csvWriter.WriteRecords(customersResult);
                    writer.Flush();

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var fileData = memoryStream.ToArray();

                    return File(fileData, "text/csv", "customers.csv", true);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
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
                            var customerDto = new CustomerAddDto
                            {
                                FullName = reader.GetString(0),
                                Email = reader.GetString(1),
                                Password = reader.GetString(2),
                                Birthday = reader.GetDateTime(3),
                                Avatar = reader.GetString(4),
                                Phone = reader.GetString(5),
                                Address = reader.GetString(6),
                                Ward = reader.GetString(7),
                                District = reader.GetString(8),
                            };
                            await _customerService.Create(_mapper.Map<Customer>(customerDto));
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
                        var customerDto = new CustomerAddDto
                        {
                            FullName = csv.GetField(0),
                            Email = csv.GetField(1),
                            Password = csv.GetField(2),
                            Birthday = csv.GetField<DateTime>(3),
                            Avatar = csv.GetField(4),
                            Phone = csv.GetField(5),
                            Address = csv.GetField(6),
                            Ward = csv.GetField(7),
                            District = csv.GetField(8),
                        };
                        await _customerService.Create(_mapper.Map<Customer>(customerDto));
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


        [HttpPost]
        public async Task<ActionResult> Create(CustomerAddDto customerDto)
        {
            try
            {
                var customer = await _customerService.Create(_mapper.Map<Customer>(customerDto));

                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Customer created successfully.",
                    Data = _mapper.Map<CustomerResultDto>(customer)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = ex.Message,
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(CustomerUpdateDto customerDto)
        {
            try
            {
                var customerEdit = await _customerService.GetById(customerDto.CustomerID);
                _mapper.Map(customerDto, customerEdit);

                var customer = await _customerService.Update(customerEdit);

                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Customer updated successfully.",
                    Data = _mapper.Map<CustomerResultDto>(customer)
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
                var customer = await _customerService.GetById(id);

                await _customerService.Delete(customer);
                return Ok(new ApiResponse
                {
                    StatusCode = 200,
                    Message = "Customer removed successfully."
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

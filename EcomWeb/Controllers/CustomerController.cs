using AutoMapper;
using EcomWeb.Dtos.Customer;
using EcomWeb.Dtos.Product;
using EcomWeb.Models;
using EcomWeb.Services;
using Microsoft.AspNetCore.Mvc;

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

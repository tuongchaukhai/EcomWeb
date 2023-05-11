using EcomWeb.Dtos.Customer;
using EcomWeb.Models;
using EcomWeb.Repository;

namespace EcomWeb.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Create(Customer customer)
        {
            if (GetByEmail(customer.Email).Result != null)
                throw new Exception("This email already exists.");

            await _customerRepository.Create(customer);
            return customer;
        }

        public async Task<bool> Delete(Customer customer)
        {
            if (GetById(customer.CustomerId).Result == null)
                throw new Exception("This id doesn't exists.");

            await _customerRepository.Delete(customer);
            return true;
        }

        public async Task<CustomersPage> GetAll(int page = 1, int pageSize = 10)
        {
            return await _customerRepository.GetAll(page, pageSize);
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await _customerRepository.GetByEmail(email);
        }

        public async Task<Customer> GetById(int id)
        {
            var result = await _customerRepository.GetById(id);
            if(result == null)
                throw new Exception("This id doesn't exists.");
            return result;
        }

        public async Task<Customer> Update(Customer customer)
        {
            if (GetById(customer.CustomerId).Result != null)
                throw new Exception("This id doesn't exists.");

            await _customerRepository.Update(customer);
            return customer;

        }
    }
}

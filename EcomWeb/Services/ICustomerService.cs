using EcomWeb.Dtos.Customer;
using EcomWeb.Dtos.Product;
using EcomWeb.Models;

namespace EcomWeb.Services
{
    public interface ICustomerService
    {
        Task<CustomersPage> GetAll(int page = 1, int pageSize = 10);
        Task<IEnumerable<Customer>> ExportData();
        Task<Customer> GetByEmail(string email);
        Task<Customer> GetById(int id);
        Task<Customer> Create(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<bool> Delete(Customer customer);
    }
}

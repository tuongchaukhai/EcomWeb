using EcomWeb.Dtos.Customer;
using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public interface ICustomerRepository: IRepositoryBase<Customer>
    {
        new Task<CustomersPage> GetAll(int page = 1, int pageSize = 10);

        Task<IEnumerable<Customer>> ExportData();

        Task<Customer> GetByEmail(string email);
    }
}

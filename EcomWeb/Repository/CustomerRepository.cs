using EcomWeb.Dtos.Customer;
using EcomWeb.Dtos.User;
using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomWeb.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyDbContext context) : base(context)
        {
        }

        public async Task<CustomersPage> GetAll(int page = 1, int pageSize = 10)
        {
            var customers = await _context.Customers.ToListAsync();

            int totalRecords = customers.Count;

            customers = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new CustomersPage {  Customers = customers, TotalRecords = totalRecords };
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await _context.Customers.Where(b => b.Email == email).FirstOrDefaultAsync();
        }

    }
}

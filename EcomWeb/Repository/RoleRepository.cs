using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(MyDbContext context) : base(context) { }

    }
}

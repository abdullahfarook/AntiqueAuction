using System.Threading.Tasks;
using AntiqueAuction.Core.Models;

namespace AntiqueAuction.Core.Repository
{
    // Framework independent Users repository interface
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> Get(string username);
    }
}

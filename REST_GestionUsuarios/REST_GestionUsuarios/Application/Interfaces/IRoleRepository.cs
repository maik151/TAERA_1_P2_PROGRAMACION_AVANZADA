using REST_GestionUsuarios.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_GestionUsuarios.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int id);
    }
}
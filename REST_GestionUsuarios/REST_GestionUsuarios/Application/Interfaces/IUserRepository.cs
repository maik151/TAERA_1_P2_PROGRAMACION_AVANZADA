using REST_GestionUsuarios.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_GestionUsuarios.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<int> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id); // Soft Delete
        Task<User?> GetByEmailAsync(string email);
    }
}
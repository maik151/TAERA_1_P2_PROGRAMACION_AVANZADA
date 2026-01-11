using Dapper;
using REST_GestionUsuarios.Application.Interfaces;
using REST_GestionUsuarios.Domain.Entities;
using System.Data;

namespace REST_GestionUsuarios.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RoleRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM ROLES ORDER BY NAME";
            return await connection.QueryAsync<Role>(sql);
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM ROLES WHERE ID = :Id";
            return await connection.QueryFirstOrDefaultAsync<Role>(sql, new { Id = id });
        }
    }
}
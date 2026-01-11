using Dapper;
using REST_GestionUsuarios.Application.Interfaces;
using REST_GestionUsuarios.Domain.Entities;
using System.Data;

namespace REST_GestionUsuarios.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"
                SELECT 
                    u.ID, 
                    u.ROLE_ID AS RoleId, 
                    u.FIRST_NAME AS FirstName, 
                    u.LAST_NAME AS LastName, 
                    u.EMAIL AS Email, 
                    u.IS_ACTIVE AS IsActive, 
                    u.CREATED_AT AS CreatedAt,
                    r.Name as RoleName 
                FROM USERS u 
                JOIN ROLES r ON u.ROLE_ID = r.ID 
                ORDER BY u.ID DESC";

            return await connection.QueryAsync<User>(sql);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"
                SELECT 
                    ID, 
                    ROLE_ID AS RoleId, 
                    FIRST_NAME AS FirstName, 
                    LAST_NAME AS LastName, 
                    EMAIL AS Email, 
                    PASSWORD AS Password,
                    IS_ACTIVE AS IsActive, 
                    CREATED_AT AS CreatedAt
                FROM USERS 
                WHERE ID = :Id";

            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<int> CreateAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();
            // Oracle 12c+ soporta IDENTITY, usamos RETURNING INTO para obtener el ID generado
            var sql = @"
                INSERT INTO USERS (ROLE_ID, FIRST_NAME, LAST_NAME, EMAIL, PASSWORD, IS_ACTIVE, CREATED_AT) 
                VALUES (:RoleId, :FirstName, :LastName, :Email, :Password, 1, CURRENT_TIMESTAMP)
                RETURNING ID INTO :Id";

            var parameters = new DynamicParameters();
            parameters.Add("RoleId", user.RoleId);
            parameters.Add("FirstName", user.FirstName);
            parameters.Add("LastName", user.LastName);
            parameters.Add("Email", user.Email);
            parameters.Add("Password", user.Password);
            // Parámetro de salida para recuperar el ID
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(sql, parameters);
            return parameters.Get<int>("Id");
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"
                UPDATE USERS 
                SET ROLE_ID = :RoleId, 
                    FIRST_NAME = :FirstName, 
                    LAST_NAME = :LastName, 
                    EMAIL = :Email 
                WHERE ID = :Id";

            var rowsAffected = await connection.ExecuteAsync(sql, user);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            // Soft Delete: Solo actualizamos el estado
            var sql = "UPDATE USERS SET IS_ACTIVE = 0 WHERE ID = :Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();
            // Importante: Usamos los Alias (AS ...) para que Dapper mapee bien
            var sql = @"
                SELECT 
                    ID, 
                    ROLE_ID AS RoleId, 
                    FIRST_NAME AS FirstName, 
                    LAST_NAME AS LastName, 
                    EMAIL AS Email, 
                    IS_ACTIVE AS IsActive
                FROM USERS 
                WHERE EMAIL = :Email";
            // Nota: Si quieres ignorar mayúsculas/minúsculas usa: WHERE LOWER(EMAIL) = LOWER(:Email)

            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }
    }
}
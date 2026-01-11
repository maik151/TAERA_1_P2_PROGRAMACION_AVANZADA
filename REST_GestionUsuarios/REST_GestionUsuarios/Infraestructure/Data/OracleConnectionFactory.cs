using System.Data;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;
using REST_GestionUsuarios.Application.Interfaces;

namespace REST_GestionUsuarios.Infrastructure.Data
{
    public class OracleConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public OracleConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleConnection")
                                ?? throw new ArgumentNullException("La cadena de conexión 'OracleConnection' no existe en appsettings.json");
        }

        public IDbConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}
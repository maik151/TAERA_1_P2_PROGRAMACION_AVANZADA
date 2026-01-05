using System.Data;
using OracleClient = Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace Taller_REST_API.Infraestructure
{
    public class OracleConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public OracleConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IDbConnection CreateConnection() { 

            var connectionString = _configuration.GetConnectionString("OracleConnection");

            return new OracleClient.OracleConnection(connectionString);

        }




    }
}

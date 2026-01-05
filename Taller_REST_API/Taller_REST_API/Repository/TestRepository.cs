using Dapper;
using System.Data;
using Taller_REST_API.Infraestructure;

namespace Taller_REST_API.Repository
{
    public class TestRepository
    {
        private readonly IDbConnectionFactory _connectionFacttory;

        public TestRepository(IDbConnectionFactory param) { 
            _connectionFacttory = param;
        }


        public async Task<string> VerificarConexion() { 
            
            using var Oraccleconn = _connectionFacttory.CreateConnection();

            var query = "SELECT 'CONEXION EXITOS A LA BASE DE DATOS ORACLE' || TO_CHAR(SYSDATE, 'YYYY-MM-DD HH24:MI:SS') FROM DUAL" ;

            var data = await Oraccleconn.QueryFirstOrDefaultAsync<string>(query);

            return data!;
        }

    }
}

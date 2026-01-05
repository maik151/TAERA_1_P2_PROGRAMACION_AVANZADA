using Dapper;
using System.Data;
using Taller_REST_API.Entities;
using Taller_REST_API.Infraestructure;

namespace Taller_REST_API.Repository
{
    public class ActivoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory; 

        public ActivoRepository(IDbConnectionFactory param)
        {
            _connectionFactory = param;
        }

        // BUSCAR ACTIVOS
        public async Task<IEnumerable<ActivoDto>> BuscarAsync(string busqueda )
        {
            using var oracleConn = _connectionFactory.CreateConnection();

           
            var query = "SELECT activo_id AS ActivoId, codigo, nombre, fecha_compra AS FechaCompra FROM ActivoMantenimiento";

          
            var parametros = new DynamicParameters();

            if (!string.IsNullOrEmpty(busqueda))
            {
                
                query += " WHERE LOWER(codigo) LIKE :busqueda OR LOWER(nombre) LIKE :busqueda";
                parametros.Add("busqueda", $"%{busqueda.ToLower()}%", DbType.String);
            }

            return await oracleConn.QueryAsync<ActivoDto>(query, parametros);
        }

        // OBTENER POR ID
        public async Task<ActivoDto> ObtenerPorIdAsync(long id)
        {
            using var oracleConn = _connectionFactory.CreateConnection();

            var query = @"SELECT activo_id AS ActivoId, codigo, nombre, fecha_compra AS FechaCompra 
                          FROM ActivoMantenimiento 
                          WHERE activo_id = :ID";

           
            var data = await oracleConn.QueryFirstOrDefaultAsync<ActivoDto>(query, new { ID = id });
            return data;
        }

        // INSERTAR ACTIVO
        public async Task<int> InsertarActivoAsync(ActivoDto activo)
        {
            using var oracleConn = _connectionFactory.CreateConnection();


            var query = @"
                        INSERT INTO ActivoMantenimiento (activo_id, codigo, nombre, fecha_compra)
                        VALUES (SEQ_ACTIVO_ID.NEXTVAL, :Codigo, :Nombre, :FechaCompra)";


            var rowsAffected = await oracleConn.ExecuteAsync(query, activo);
            return rowsAffected;
        }

        // ACTUALIZAR ACTIVO
        public async Task<int> ActualizarActivoAsync(ActivoDto activo)
        {
            using var oracleConn = _connectionFactory.CreateConnection();

         
            var query = @"UPDATE ActivoMantenimiento SET
                            codigo = :Codigo,
                            nombre = :Nombre,
                            fecha_compra = :FechaCompra
                          WHERE activo_id = :ActivoId";

          
            var rowsAffected = await oracleConn.ExecuteAsync(query, activo);
            return rowsAffected;
        }

        // ELIMINAR ACTIVO
        public async Task<int> EliminarActivoAsync(long id)
        {
            using var oracleConn = _connectionFactory.CreateConnection();
            var query = @"DELETE FROM ActivoMantenimiento WHERE activo_id = :ID";
            var rowsAffected = await oracleConn.ExecuteAsync(query, new { ID = id });
            return rowsAffected;
        }
    }
}
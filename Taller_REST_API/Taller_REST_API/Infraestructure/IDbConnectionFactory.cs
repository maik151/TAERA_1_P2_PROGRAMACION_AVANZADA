using System.Data;

namespace Taller_REST_API.Infraestructure
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

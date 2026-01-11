using System.Data;

namespace REST_GestionUsuarios.Application.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
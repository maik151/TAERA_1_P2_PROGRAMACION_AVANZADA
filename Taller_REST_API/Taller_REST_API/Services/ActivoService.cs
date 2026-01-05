using System.Diagnostics.Eventing.Reader;
using Taller_REST_API.Entities;
using Taller_REST_API.Repository;

namespace Taller_REST_API.Services
{
    public class ActivoService
    {
        private readonly ActivoRepository _activoRepository;

        public ActivoService(ActivoRepository activoRepository)
        {
            _activoRepository = activoRepository;
        }

        //Obtener todos los Activos
        public async Task<IEnumerable<ActivoDto>> ObtrenerTodosActivos(string busqueda) { 
            return await _activoRepository.BuscarAsync(busqueda);
        }


        //Obtener por ID

        public async Task<ActivoDto> ObtenerPorId(long id) { 
            var activo = await _activoRepository.ObtenerPorIdAsync(id);
            return activo;
        }

        //InsertarActivo
        public async Task<int> InsertarActivo(ActivoDto activo) { 
            if(activo == null)
                throw new ArgumentNullException(nameof(activo), "El activo no puede ser nulo.");

            var rowsAffected = await _activoRepository.InsertarActivoAsync(activo);
            return rowsAffected;
        }

        //Actualizar Activo

        public async Task<int> ActualizarActivo(long id, ActivoDto activo) {
            if (activo == null)
                throw new ArgumentNullException(nameof(activo), "El activo no puede ser nulo.");

            var data = await _activoRepository.ObtenerPorIdAsync(id);
            if(data == null)
                throw new KeyNotFoundException($"No se encontró un activo con ID {id} para actualizar.");


            var rowsAffected = await _activoRepository.ActualizarActivoAsync(activo);
            return rowsAffected;

        }


        //EliminarActivo
        public async Task<int> EliminarActivo(long id)
        {
            
            var activo = await _activoRepository.ObtenerPorIdAsync(id);

            if(activo == null)
                throw new KeyNotFoundException($"No se encontró un activo con ID {id} para eliminar.");

            var rowsAffected = await _activoRepository.EliminarActivoAsync(id);
            return rowsAffected;

        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Taller_REST_API.Entities;
using Taller_REST_API.Repository;
using Taller_REST_API.Services;

namespace Taller_REST_API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ControllerCRUD : ControllerBase
    {


        public readonly TestRepository _testRepository;
        public readonly ActivoService _activoService;

        public ControllerCRUD(TestRepository param1, ActivoService param2) {
            _testRepository = param1;
            _activoService = param2;
        }

        [HttpGet("test-oracle")]
        public async Task<IActionResult> ProbarConexionOracle() {

            try {

                var mensaje = await _testRepository.VerificarConexion();
                return Ok(new {mensaje = mensaje, Status = 200 });

            }
            catch (Exception ex) {
                return BadRequest(new { mensaje = ex.Message, Status = 400 });
            }

        }



        [HttpGet("obtener")]
        public async Task<IActionResult> Obtener([FromQuery] string? busqueda = null)
        {
            try
            {
                
                var resultado = await _activoService.ObtrenerTodosActivos(busqueda);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }



        [HttpGet("obtener/{id}")]
        public async Task<IActionResult> ObtenerPorId(long id)
        {
            var data = await _activoService.ObtenerPorId(id);
            return Ok(data);
        }


        [HttpPost("crear")]
        public async Task<IActionResult> CrearActivo([FromBody] ActivoDto activo)
        {

            if (!ModelState.IsValid)
            {
                // Esto te dirá exactamente qué campo está mal en el JSON
                return BadRequest(ModelState);
            }
            try {

                var data = await _activoService.InsertarActivo(activo);
                return Ok(new { Mensaje = "ACTIVO CREADO EXITOSAMENTE" });
            }
            catch { 
                return BadRequest(new { Mensaje = "ERROR AL CREAR EL ACTIVO" });

            }
        }



        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> ActualizarActivo(long id, [FromBody] ActivoDto activo)
        {
            try
            {

                var data = await _activoService.ActualizarActivo(id, activo);
                return Ok(new { Mensaje = "ACTIVO ACTUALIZADO EXITOSAMENTE" });
            }
            catch
            {
                return BadRequest(new { Mensaje = "ERROR AL ACTUALIZAR EL ACTIVO" });

            }
        }


        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarActivo(long id)
        {
            try
            {
                var data = await _activoService.EliminarActivo(id);
                return Ok(new { Mensaje = "ACTIVO ELIMINADO EXITOSAMENTE" });
            }
            catch
            {
                return BadRequest(new { Mensaje = "ERROR AL ELIMINAR EL ACTIVO" });
            }
        }









    }
}

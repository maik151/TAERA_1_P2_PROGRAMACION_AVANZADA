using Microsoft.AspNetCore.Mvc;
using REST_GestionUsuarios.Application.Interfaces;
using REST_GestionUsuarios.Domain.Entities;
using Oracle.ManagedDataAccess.Client; // Para capturar excepciones específicas de Oracle

namespace REST_GestionUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        // Inyección de Dependencias del Repositorio
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return Ok(users); // Retorna HTTP 200 con la lista
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound($"El usuario con ID {id} no existe."); // HTTP 404
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null) return BadRequest();

            // 1. VALIDACIÓN DE DUPLICADOS
            // Usamos el método que acabamos de crear
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);

            if (existingUser != null)
            {
                // Si el usuario existe, devolvemos 409 Conflict
                return Conflict("El correo electrónico ya está registrado.");
            }

            if (!user.IsStrongPassword())
            {
                return BadRequest("La contraseña es muy débil. Debe tener 8 caracteres y una mayúscula.");
            }

            if (!user.HasCorporateEmail())
            {
                return BadRequest("Solo se permiten correos corporativos (@miempresa.com).");
            }

            // 2. CREACIÓN (Si no existe)
            var createdId = await _userRepository.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = createdId }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            // 1. Aseguramos que el ID del objeto coincida con la URL
            user.Id = id;

            // Validación básica
            if (user == null) return BadRequest("Datos inválidos.");

            try
            {
                var updated = await _userRepository.UpdateAsync(user);

                if (!updated)
                {
                    return NotFound($"No se pudo actualizar. Usuario {id} no encontrado.");
                }

                return NoContent(); // 204: Todo correcto
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userRepository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent(); // HTTP 204
        }
    }
}
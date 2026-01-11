using System;

namespace REST_GestionUsuarios.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Propiedad opcional para traer el nombre del rol en consultas
        public string? RoleName { get; set; }



        public bool IsStrongPassword()
        {
            
            if (string.IsNullOrEmpty(Password)) return false;
            return Password.Length >= 8 && Password.Any(char.IsUpper);
        }

        public bool HasCorporateEmail()
        {
            if (string.IsNullOrEmpty(Email)) return false;
            return Email.EndsWith("@miempresa.com", StringComparison.OrdinalIgnoreCase);
        }
    }



}
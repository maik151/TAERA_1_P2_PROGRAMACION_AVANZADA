using Xunit;
using REST_GestionUsuarios.Domain.Entities; // Asegúrate de usar el namespace correcto de tu entidad User

namespace REST_GestionUsuarios.Tests
{
    public class UserDomainTests
    {
        [Fact]
        public void IsStrongPassword_Should_Return_False_If_No_Uppercase()
        {
            var user = new User { Password = "pass123" };
            bool result = user.IsStrongPassword();
            Assert.False(result, "La contraseña debió ser rechazada por no tener mayúsculas.");
        }

        [Fact]
        public void IsStrongPassword_Should_Return_True_If_Valid()
        {
            var user = new User { Password = "Password123" };
            bool result = user.IsStrongPassword();
            Assert.True(result, "La contraseña válida fue rechazada incorrectamente.");
        }

        [Fact]
        public void HasCorporateEmail_Should_Return_False_If_Public_Domain()
        {
            // Arrange
            var user = new User { Email = "empleado@gmail.com" };

            // Act
            bool result = user.HasCorporateEmail();

            // Assert
            Assert.False(result, "El email público debió ser rechazado.");
        }


        [Fact]
        public void HasCorporateEmail_Should_Return_True_If_Valid()
        {
            // Arrange
            var user = new User { Email = "gerencia@miempresa.com" };

            // Act
            bool result = user.HasCorporateEmail();

            // Assert
            Assert.True(result, "El email corporativo válido fue rechazado incorrectamente.");
        }


    }
}
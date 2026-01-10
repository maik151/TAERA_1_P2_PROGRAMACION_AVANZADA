describe('Formulario de Usuario', () => {
  it('Debe mostrar alerta de error en correos duplicados', () => {
    // 1. Navegar a la aplicación
    cy.visit('http://localhost:4200');

    // 2. Abrir el formulario (Selector corregido basado en tu HTML)
    // Buscamos el botón que contenga el texto exacto "Nuevo Usuario"
    cy.contains('button', 'Nuevo Usuario').click();

    // 3. Validar que el modal se abrió chequeando un campo visible
    cy.get('input[formcontrolname="firstName"]').should('be.visible');

    // 4. Llenar el formulario completo (requerido para habilitar el botón Guardar)
    cy.get('input[formcontrolname="firstName"]').type('Admin');
    cy.get('input[formcontrolname="lastName"]').type('Test');
    cy.get('input[formcontrolname="email"]').type('admin@test.com'); // Correo existente
    cy.get('select[formcontrolname="roleId"]').select(1); // Seleccionar un rol (index)
    cy.get('input[formcontrolname="password"]').type('Password123!');

    // 5. Enviar el formulario
    cy.contains('button', 'Guardar Usuario').click();

    // 6. Validar que la UI muestre el mensaje de error del Backend
    // Esperamos que aparezca la alerta roja
    cy.contains('El correo electrónico ya está registrado').should('be.visible');
  });
});
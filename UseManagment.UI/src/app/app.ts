import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
// Importamos el componente VISUAL (la tabla), no el servicio
import { UserListComponent } from './features/user-list/user-list';

@Component({
  selector: 'app-root',
  standalone: true,
  // Aquí decimos: "Este componente va a usar la Tabla de Usuarios"
  imports: [RouterOutlet, CommonModule, UserListComponent], 
  templateUrl: './app.html',
  styleUrl: './app.scss' // Asegúrate si usas .css o .scss según lo que elegiste al crear el proyecto
})
export class App {
  title = 'UserManagement.UI';
}
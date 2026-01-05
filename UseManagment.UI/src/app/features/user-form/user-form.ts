import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserApiService } from '../../core/services/user-api';
import { User, Role } from '../../core/models/api-models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-form.html',
  styleUrl: './user-form.scss'
})


export class UserForm implements OnInit {
  private fb = inject(FormBuilder);
  private userService = inject(UserApiService);

  // Recibimos un usuario si es edición (puede ser null si es crear)
  @Input() userToEdit: User | null = null;
  
  // Evento para avisar al padre que cerramos el modal (true = recargar lista)
  @Output() close = new EventEmitter<boolean>();

  form: FormGroup;
  roles: Role[] = [];
  isSubmitting = false;
  errorMessage: string = '';

  constructor() {
    // Definición inicial del formulario con validaciones
    this.form = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      roleId: [null, [Validators.required]], // El select de roles
      password: [''], // La contraseña tiene validación condicional
      isActive: [true]
    });
  }

  


  ngOnInit(): void {
    this.loadRoles();

    // Si nos pasaron un usuario, llenamos el formulario (Modo Edición)
    if (this.userToEdit) {
      this.form.patchValue(this.userToEdit);
      // En edición, la contraseña es opcional
      this.form.get('password')?.clearValidators();
    } else {
      // En creación, la contraseña es obligatoria
      this.form.get('password')?.setValidators([Validators.required, Validators.minLength(4)]);
    }
    this.form.get('password')?.updateValueAndValidity();
  }

  loadRoles() {
    this.userService.getRoles().subscribe({
      next: (data) => this.roles = data,
      error: (err) => console.error('Error cargando roles', err)
    });
  }

  save() {
    this.errorMessage = ''; // Limpiamos errores previos al intentar guardar
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const formData = this.form.value as User;

    let request$: Observable<any>;

    if (this.userToEdit) {
      request$ = this.userService.updateUser(this.userToEdit.id!, formData);
    } else {
      request$ = this.userService.createUser(formData);
    }

    request$.subscribe({
      next: () => {
        this.isSubmitting = false;
        this.close.emit(true);
      },
      // 2. MODIFICAMOS EL MANEJO DE ERRORES
      error: (err: any) => {
        this.isSubmitting = false;
        console.error('Error API:', err);

        // Si el servidor responde 409 (Conflicto), mostramos su mensaje
        if (err.status === 409) {
          // err.error suele contener el texto "El correo electrónico ya está registrado"
          this.errorMessage = err.error || 'Este correo ya está en uso.'; 
        } else {
          // Para otros errores (500, 400, etc)
          this.errorMessage = 'Ocurrió un error inesperado. Inténtalo más tarde.';
        }
      }
    });
  }

  cancel() {
    this.close.emit(false); // Emitimos false: solo cerrar, no recargar
  }
}
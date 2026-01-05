import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserApiService } from '../../core/services/user-api';
import { User } from '../../core/models/api-models';
import { UserForm } from '../user-form/user-form'; 

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, UserForm], 
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss'
})

export class UserListComponent implements OnInit {
  // Inyección del servicio
  private userService = inject(UserApiService);
  users: User[] = [];
  isLoading = true;
  isModalOpen = false;
  selectedUser: User | null = null;

  avatarColors = [
    'bg-red-500',    'bg-orange-500', 'bg-amber-500',
    'bg-green-500',  'bg-emerald-500','bg-teal-500',
    'bg-cyan-500',   'bg-blue-500',   'bg-indigo-500',
    'bg-violet-500', 'bg-purple-500', 'bg-fuchsia-500',
    'bg-pink-500',   'bg-rose-500'
  ];



  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.isLoading = true;
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al cargar usuarios:', err);
        this.isLoading = false;
      }
    });
  }

  deleteUser(id: number | undefined) {
    if (!id) return;
    
    // Un confirm nativo por ahora (luego podemos hacerlo más bonito)
    if (confirm('¿Estás seguro de eliminar este usuario? Esta acción no se puede deshacer.')) {
      this.userService.deleteUser(id).subscribe(() => {
        this.loadUsers();
      });
    }
  }

  getInitials(user: User): string {
    
    const first = user.firstName?.charAt(0) || '';
    const last = user.lastName?.charAt(0) || '';
    return (first + last).toUpperCase() || '?';
  }


  getAvatarColor(id?: number): string {
    if (!id) return 'bg-slate-400'; 
    const index = id % this.avatarColors.length;
    return this.avatarColors[index];
  }


  openModal(user?: User) {
    this.selectedUser = user || null;
    this.isModalOpen = true;
  }

  handleModalClose(refresh: boolean) {
    this.isModalOpen = false;
    this.selectedUser = null;
    if (refresh) {
      this.loadUsers(); 
    }
  }
}

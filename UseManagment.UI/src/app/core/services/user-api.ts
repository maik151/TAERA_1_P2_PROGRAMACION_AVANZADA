import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { User, Role } from '../models/api-models'; 

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  
  private http = inject(HttpClient);
  //private readonly apiUrl = 'https://localhost:7179/api'; 
  private readonly apiUrl = `${environment.apiUrl}`;

  constructor() { }

  // --- USERS ---
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/Users`);
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/Users/${id}`);
  }

  createUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/Users`, user);
  }

  updateUser(id: number, user: User): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/Users/${id}`, user);
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Users/${id}`);
  }

  // --- ROLES ---
  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.apiUrl}/Roles`);
  }
}
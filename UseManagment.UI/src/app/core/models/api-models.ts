// AQUÍ SOLO DEFINICIONES DE DATOS (CONTRATOS)

export interface Role {
  id: number;
  name: string;
  description: string;
}

export interface User {
  id?: number;
  roleId: number;
  roleName?: string;
  firstName: string;
  lastName: string;
  email: string;
  password?: string;
  isActive: boolean;
  createdAt?: string;
}
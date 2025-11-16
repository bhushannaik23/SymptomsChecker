export interface LoginRequest {
  email: string;
  password: string;
  userType: string;
}

export interface LoginResponse {
  token: string;
  userType: string;
  userId: number;
  name: string;
  email: string;
}
export interface RegisterPatientRequest {
  name: string;
  email: string;
  password: string;
  phone: string;
}
export interface RegisterDoctorRequest {
  name: string;
  email: string;
  password: string;
  phone: string;
  specialtyIds: number[];
}

export interface RegisterAdminRequest {
  name: string;
  email: string;
  password: string;
}

export interface User {
  userId: number;
  name: string;
  email: string;
  userType: 'patient' | 'doctor' | 'admin';
}

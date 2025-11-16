import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { 
    path: 'login', 
    loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent)
  },
  { 
    path: 'register', 
    loadComponent: () => import('./components/register/register.component').then(m => m.RegisterComponent)
  },
  {
    path: 'patient',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./components/patient/dashboard/dashboard.component').then(m => m.PatientDashboardComponent)
      },
      {
        path: 'symptoms',
        loadComponent: () => import('./components/patient/symptoms/symptoms.component').then(m => m.SymptomsComponent)
      },
      {
        path: 'appointments',
        loadComponent: () => import('./components/patient/appointments/appointments.component').then(m => m.PatientAppointmentsComponent)
      },
      {
        path: 'profile',
        loadComponent: () => import('./components/patient/profile/profile.component').then(m => m.PatientProfileComponent)
      }
    ]
  },
  {
    path: 'doctor',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./components/doctor/dashboard/dashboard.component').then(m => m.DoctorDashboardComponent)
      },
      {
        path: 'appointments',
        loadComponent: () => import('./components/doctor/appointments/appointments.component').then(m => m.DoctorAppointmentsComponent)
      },
      {
        path: 'profile',
        loadComponent: () => import('./components/doctor/profile/profile.component').then(m => m.DoctorProfileComponent)
      }
    ]
  },
  {
    path: 'admin',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./components/admin/dashboard/dashboard.component').then(m => m.AdminDashboardComponent)
      },
      {
        path: 'doctors',
        loadComponent: () => import('./components/admin/doctors/doctors.component').then(m => m.AdminDoctorsComponent)
      },
      {
        path: 'patients',
        loadComponent: () => import('./components/admin/patients/patients.component').then(m => m.AdminPatientsComponent)
      },
      {
        path: 'profile',
        loadComponent: () => import('./components/admin/profile/profile.component').then(m => m.AdminProfileComponent)
      }
    ]
  }
];

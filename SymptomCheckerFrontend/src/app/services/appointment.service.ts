import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import {
  Appointment,
  CreateAppointmentRequest,
  UpdateAppointmentStatusRequest,
} from '../models/appointment.model';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor(private apiService: ApiService) {}

  getPatientAppointments(patientId: number): Observable<Appointment[]> {
    return this.apiService.get<Appointment[]>(`appointments/patient/${patientId}`);
  }

  getDoctorAppointments(doctorId: number): Observable<Appointment[]> {
    return this.apiService.get<Appointment[]>(`appointments/doctor/${doctorId}`);
  }

  createAppointment(appointment: CreateAppointmentRequest): Observable<Appointment> {
    return this.apiService.post<Appointment>('appointments', appointment);
  }

  updateAppointmentStatus(
    appointmentId: number,
    status: UpdateAppointmentStatusRequest
  ): Observable<Appointment> {
    return this.apiService.put<Appointment>(`appointments/${appointmentId}/status`, status);
  }
}

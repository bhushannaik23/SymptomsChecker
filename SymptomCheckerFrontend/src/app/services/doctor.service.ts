import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import { Doctor, UpdateDoctorRequest } from '../models/doctor.model';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  constructor(private apiService: ApiService) {}

  getAllDoctors(): Observable<Doctor[]> {
    return this.apiService.get<Doctor[]>('doctors');
  }

  getDoctorById(id: number): Observable<Doctor> {
    return this.apiService.get<Doctor>(`doctors/${id}`);
  }

  updateDoctor(id: number, doctor: UpdateDoctorRequest): Observable<Doctor> {
    return this.apiService.put<Doctor>(`doctors/${id}`, doctor);
  }

  getDoctorsBySpecialty(specialty: string): Observable<Doctor[]> {
    return this.apiService.get<Doctor[]>(`doctors/specialty/${specialty}`);
  }

  deleteDoctor(id: number): Observable<string> {
    return this.apiService.delete<string>(`admin/doctors/${id}`);
  }
}
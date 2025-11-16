import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/services/api.service';
import { Patient, CreatePatient, UpdatePatientRequest } from '../models/patient.model';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  constructor(private apiService: ApiService) {}

  getAllPatients(): Observable<Patient[]> {
    return this.apiService.get<Patient[]>('patients');
  }

  getPatientById(id: number): Observable<Patient> {
    return this.apiService.get<Patient>(`patients/${id}`);
  }

  createPatient(patient: CreatePatient): Observable<Patient> {
    return this.apiService.post<Patient>('patients', patient);
  }

  updatePatient(id: number, patient: UpdatePatientRequest): Observable<Patient> {
    return this.apiService.put<Patient>(`patients/${id}`, patient);
  }

  deletePatient(id: number): Observable<string> {
    return this.apiService.delete<string>(`admin/patients/${id}`);
  }
}